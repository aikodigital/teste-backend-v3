using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.DTOs;
using TheatricalPlayersRefactoringKata.API.infra;
using TheatricalPlayersRefactoringKata.Models;
namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly ApiDbContext _db;

        public InvoiceController(ApiDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            if (invoiceDto == null)
            {
                return BadRequest("Invoice data is null.");
            }

            var invoice = new Invoice
            {
                Customer = invoiceDto.Customer,
                Performances = new List<Performance>()
            };

            foreach (var performanceDto in invoiceDto.Performances)
            {
                var play = _db.Plays.SingleOrDefault(p => p.Name == performanceDto.PlayId);

                if (play == null)
                {
                    return BadRequest($"Play with ID {performanceDto.PlayId} not found.");
                }

                var performance = new Performance
                {
                    PlayId = play.Name,  // Associar o Play usando o ID
                    Audience = performanceDto.Audience,
                    Invoice = invoice
                };

                invoice.Performances.Add(performance);
            }

            _db.Invoices.Add(invoice);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        public IActionResult GetInvoice(int id)
        {
            var invoice = _db.Invoices
                .Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _db.Invoices
                .Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .ToListAsync();
            return Ok(invoices);
        }
    }
}