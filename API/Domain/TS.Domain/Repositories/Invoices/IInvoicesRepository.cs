using TS.Domain.Entities;

namespace TS.Domain.Repositories.Invoices
{
    public interface IInvoicesRepository
    {
        Task CreateAsync(Invoice entity);
        Task<Invoice?> GetAsync(long id);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task UpdateAsync(Invoice entity);
        Task DeleteAsync(long id);
    }
}