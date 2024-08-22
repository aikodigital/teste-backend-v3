using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetInvoiceAsync(int invoiceId);
        Task<Dictionary<string, Play>> GetPlaysAsync();
    }
}
