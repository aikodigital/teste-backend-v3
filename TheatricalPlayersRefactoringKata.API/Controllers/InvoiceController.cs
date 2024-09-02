using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.API.DTO;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Persistence;
using TheatricalPlayersRefactoringKata.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IBaseRepository<Invoice> _invoiceRepository;
        private readonly DBContext _context;

        public InvoiceController(IBaseRepository<Invoice> repository, DBContext context)
        {
            _invoiceRepository = repository;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InvoiceDTO invoiceDTO)
        {
            var invoice = new Invoice
            {
                Customer = invoiceDTO.Customer,
                Performances = new List<Performance>()
            };

            foreach (var performance in invoiceDTO.Performances)
            {
                var play = _context.Plays.FirstOrDefault(p => p.Name == performance.PlayId);

                if (play == null)                
                    return BadRequest($"Play ID [{performance.PlayId}] not found.");

                var _performance = new Performance
                {
                    PlayId = play.Name,
                    Audience = performance.Audience,
                    Invoice = invoice
                };

                invoice.Performances.Add(_performance);
            }

            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            //await _invoiceRepository.AddAsync(invoice);
            return CreatedAtAction(nameof(Get), new { id = invoice.Id }, invoice);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var invoices = await _context.Invoices.ToListAsync();
            return Ok(invoices);
        }
    }
}
