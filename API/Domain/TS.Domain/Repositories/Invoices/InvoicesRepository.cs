using Microsoft.EntityFrameworkCore;
using TS.Domain.Entities;
using TS.Domain.EntityFramework;

namespace TS.Domain.Repositories.Invoices
{
    public class InvoicesRepository(AppDbContext context) : IInvoicesRepository
    {
        private readonly AppDbContext _context = context;

        public async Task CreateAsync(Invoice entity)
        {
            _context.Invoices.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var invoices = await GetAsync(id);

            if (invoices != null)
            {
                _context.Invoices.Remove(invoices);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Invoice?> GetAsync(long id)
        {
            var invoices = await _context.Invoices.Include(i => i.InvoicesItems).FirstOrDefaultAsync(x => x.Id == id);
            return invoices;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            var invoices = await _context.Invoices.ToListAsync();
            return invoices;
        }

        public async Task UpdateAsync(Invoice entity)
        {
            _context.Invoices.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}