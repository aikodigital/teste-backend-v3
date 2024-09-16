using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Context;
using TheatricalPlayersRefactoringKata.Interface;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly TheatricalContext _context;

        public InvoiceRepository(TheatricalContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice.Id;
        }

        public async Task<Invoice?> Get(int id)
        {
            return await _context.Invoices
                .Include(invoice => invoice.Performances)
                .FirstOrDefaultAsync(invoice => invoice.Id == id);
        }

        public void Delete(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
        }

        public List<Invoice> GetAll(int take, int page)
        {
            return _context.Invoices
                .Include(invoice => invoice.Performances)
                .Skip(page - 1).Take(take).ToList();
        }
    }
}
