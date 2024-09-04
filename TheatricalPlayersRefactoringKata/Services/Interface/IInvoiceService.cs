using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services.Interface
{
    public interface IInvoiceService
    {
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
        Task<Invoice> UpdateInvoiceAsync(Invoice invoice);
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task DeleteInvoiceAsync(int id);
    }
}
