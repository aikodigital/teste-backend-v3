using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public interface IInvoiceService
    {
        Task<string> GenerateInvoiceXmlAsync(InvoiceRequest invoiceRequest);
    }
}
