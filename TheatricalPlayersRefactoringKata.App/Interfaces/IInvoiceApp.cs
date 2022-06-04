using TheatricalPlayersRefactoringKata.App.Model.Request;
using TheatricalPlayersRefactoringKata.App.Model.Response;
using TheatricalPlayersRefactoringKata.Domain.Model.Enum;

namespace TheatricalPlayersRefactoringKata.App.Interfaces
{
    public interface IInvoiceApp
    {
        Task<Response<NewInvoiceResponse>> Invoice(Request<NewInvoiceRequest> request);
        Task<Response<string>> GenerateExtract(long invoiceId, ExtractTypeEnum extractType);
    }
}