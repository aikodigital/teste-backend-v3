using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Repositories.Interface;

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
            _dbContext.Invoices.Update(invoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await GetInvoiceByIdAsync(id);
            if (invoice != null)
            {
                _dbContext.Invoices.Remove(invoice);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
