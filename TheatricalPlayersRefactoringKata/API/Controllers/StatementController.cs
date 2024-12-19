using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.API.DTOs;
using TheatricalPlayersRefactoringKata.API.Queue;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/v1/statements")] 
    public class StatementController : ControllerBase
    {
        private readonly ITheaterStatementProcessingQueue _queue;
        private readonly TheaterAppDbContext _dbContext;
        private readonly ITheaterStatementExportService _exportService;

        public StatementController(
            ITheaterStatementProcessingQueue  queue,
            TheaterAppDbContext dbContext,
            ITheaterStatementExportService exportService)
        {
            _queue = queue;
            _dbContext = dbContext;
            _exportService = exportService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostInvoice([FromBody] TheaterInvoiceRequestDTO invoiceRequest)
        {
            if (invoiceRequest == null ||
                string.IsNullOrEmpty(invoiceRequest.Customer) ||
                invoiceRequest.Performances == null || !invoiceRequest.Performances.Any())
            {
                return BadRequest(new { message = "Customer and performances are required." });
            }

            string invoiceJson = JsonSerializer.Serialize(invoiceRequest);

            try
            {
                await _queue.EnqueueAsync(invoiceJson);
                return Accepted(new { message = "Invoice enqueued for processing." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while enqueuing the invoice: {ex.Message}" });
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetInvoice(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid invoice ID." });
            }

            var invoice = _dbContext.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound(new { message = "Invoice not found." });
            }

            var invoiceDto = new TheaterInvoiceResponseDTO
            {
                InvoiceId = invoice.Id,
                Customer = invoice.Customer,
                StatementXml = invoice.StatementXml,
                ProcessedAt = invoice.ProcessedAt
            };

            return Ok(invoiceDto);
        }

        [HttpGet("{id:int}/xml")]
        public IActionResult GetInvoiceXml(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid invoice ID." });
            }

            var invoice = _dbContext.Invoices
                .Include(i => i.Performances) 
                .FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return NotFound(new { message = "Invoice not found." });
            }

            var invoiceDto = new TheaterInvoiceResponseDTO
            {
                InvoiceId = invoice.Id,
                Customer = invoice.Customer,
                StatementXml = invoice.StatementXml,
                ProcessedAt = invoice.ProcessedAt,
                Performances = invoice.Performances
            };

            try
            {
                var xml = _exportService.GerarExtratoXml(invoiceDto);
                return Content(xml, "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while generating the XML: {ex.Message}", details = ex.ToString() });
            }
        }
    }
}
