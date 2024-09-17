using Microsoft.EntityFrameworkCore;
using TS.Domain.Entities;
using TS.Domain.EntityFramework;

namespace TS.Domain.Repositories.Customers
{
    public class CustomersRepository(AppDbContext context) : ICustomersRepository
    {
        private readonly AppDbContext _context = context;

        public async Task CreateAsync(Customer entity)
        {
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var customers = await GetAsync(id);

            if (customers != null)
            {
                _context.Customers.Remove(customers);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Customer?> GetAsync(long id)
        {
            var customers = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            return customers;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        public async Task UpdateAsync(Customer entity)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}