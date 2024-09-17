using Microsoft.AspNetCore.Mvc;
using TP.ApplicationServices.Interfaces;
using TP.Domain.Entities;
using TP.Service;

namespace TP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;
        private readonly IStatementPrinterServices _statementPrinterServices;

        public InvoiceController(InvoiceService invoiceService, IStatementPrinterServices statementPrinterServices)
        {
            _invoiceService = invoiceService;
            _statementPrinterServices = statementPrinterServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpGet("{id}/{format}")]
        public IActionResult GetInvoiceStatement(Guid id, Dictionary<string, Play> plays, string format)
        {
            try
            {
                var invoice = _invoiceService.GetInvoiceByIdAsync(id).Result;

                if (invoice == null)
                {
                    return NotFound("Invoice not found");
                }

                var statement = _statementPrinterServices.Print(invoice, plays, format);
                return Ok(statement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] Invoice invoice)
        {
            await _invoiceService.AddInvoiceAsync(invoice);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.Id }, invoice);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInvoice(Guid id, [FromBody] Invoice invoice)
        {
            if (id != invoice.Id) return BadRequest();
            _invoiceService.UpdateInvoice(invoice);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(Guid id)
        {
            _invoiceService.DeleteInvoice(id);
            return NoContent();
        }
    }
}
