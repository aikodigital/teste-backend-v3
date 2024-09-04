using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Repositories.Interface;
using System.Linq;

namespace TheatricalPlayersRefactoringKata.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoiceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            _dbContext.Invoices.Add(invoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            return await _dbContext.Invoices.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _dbContext.Invoices.ToListAsync();
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            var local = _dbContext.Set<Invoice>()
                          .Local
                          .FirstOrDefault(entry => entry.Id.Equals(invoice.Id));

            if (local != null)
                _dbContext.Entry(local).State = EntityState.Detached;

            _dbContext.Invoices.Attach(invoice);
            _dbContext.Entry(invoice).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await GetInvoiceByIdAsync(id);
            if (invoice != null)
            {
                if (invoice.Performances != null && invoice.Performances.Count != 0)
                    _dbContext.Performances.RemoveRange(invoice.Performances);

                _dbContext.Invoices.Remove(invoice);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
