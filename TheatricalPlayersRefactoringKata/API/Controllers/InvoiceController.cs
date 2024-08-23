using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.API.Repositories;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.API.Repositories.Validators;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(IInvoiceRepository repo) : ControllerBase
    {
        // GET: api/Invonce
        [HttpGet]
        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            return await repo.GetInvoices();
        }

        // GET: api/Invonce/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetInvoice(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            return await repo.GetInvoiceById(id);
        }

        // POST: api/Invonce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostInvoice(InvoiceRequest invoice)
        {
            return InvoiceValidator.IsValid(invoice) ? await repo.CreateInvoice(invoice) : BadRequest();
        }

        // DELETE: api/Invonce/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            return await repo.DeleteInvoice(id);
        }
        
        // POST: api/Invonce/5/GenerateStatement
        [HttpPost("{id:guid}/GenerateStatement")]
        public async Task<IActionResult> GenerateStatement(Guid id, ReceiptType receiptType)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            return await repo.GenerateStatement(id, receiptType);
        }
    }
}
