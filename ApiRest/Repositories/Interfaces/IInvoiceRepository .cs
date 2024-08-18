using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Repositories.Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<Invoice> GetByCustomerAsync(string customer);
    }
}
