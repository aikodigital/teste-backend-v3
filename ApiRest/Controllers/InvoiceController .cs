using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.infra;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.DTOs;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private ApiDbContext _db;

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

            var performances = _db.Performances.Where(p => invoiceDto.PerformanceIds.Contains(p.Id)).ToList();

            if (performances.Count != invoiceDto.PerformanceIds.Count)
            {
                return BadRequest("One or more performance IDs are invalid.");
            }

            var invoice = new Invoice
            {
                Customer = invoiceDto.Customer,
                Performances = performances
            };

            _db.Invoices.Add(invoice);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoice(int id)
        {
            var invoice = _db.Invoices.Include(i => i.Performances).FirstOrDefault(i => i.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _db.Invoices.Include(i => i.Performances).ToListAsync();
            return Ok(invoices);
        }
    }
}
