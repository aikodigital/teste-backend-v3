using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.DTOs.InvoiceDTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs;
using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<ServiceResponse<InvoiceResponse>> CreateInvoice(InvoiceRequest invoiceRequest);
        Task<(byte[], string, string)> GenerateStatement(Guid invoiceId, Formats format);
        Task<ServiceResponse<IEnumerable<InvoiceResponse>>> GetInvoices();

    }
}
