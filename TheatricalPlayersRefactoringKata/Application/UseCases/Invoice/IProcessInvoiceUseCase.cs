using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Request;
using TheatricalPlayersRefactoringKata.Communication.Response;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Invoice;

public interface IProcessInvoiceUseCase
{
    Task<InvoiceResponse> ExecuteAsync(InvoiceRequest request);
  
}
