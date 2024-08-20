using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.DTOs.InvoiceDTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<ServiceResponse<InvoiceResponse>> CreateInvoice(InvoiceRequest invoiceRequest);
    }
}
