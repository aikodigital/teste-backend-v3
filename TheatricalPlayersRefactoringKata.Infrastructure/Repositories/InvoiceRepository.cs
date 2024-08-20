using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateInvoice(Invoice invoice)
        {
            foreach (var performance in invoice.Performances)
            {
                _context.Performances.Attach(performance);
            }
            
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<Invoice?> GetInvoiceById(Guid invoiceId)
        {
            return await _context.Invoices
                .AsNoTracking()
                .Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .FirstOrDefaultAsync(i => i.Id == invoiceId);
        }

        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            return await _context.Invoices
                .AsNoTracking()
                .Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .ToListAsync();
        }
    }
}