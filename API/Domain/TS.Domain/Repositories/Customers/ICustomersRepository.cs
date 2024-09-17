using TS.Domain.Entities;

namespace TS.Domain.Repositories.Customers
{
    public interface ICustomersRepository
    {
        Task CreateAsync(Customer entity);
        Task<Customer?> GetAsync(long id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task UpdateAsync(Customer entity);
        Task DeleteAsync(long id);
    }
}