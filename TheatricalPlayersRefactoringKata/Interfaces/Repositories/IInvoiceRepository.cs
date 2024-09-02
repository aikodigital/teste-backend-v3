using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Interfaces.Repositories
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Task<Invoice> GetByCustomerNameAsync(string costumer);
    }
}
