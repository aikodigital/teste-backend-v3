using TheatricalPlayersRefactoringKata.App.Model.Request;
using TheatricalPlayersRefactoringKata.App.Model.Response;

namespace TheatricalPlayersRefactoringKata.App.Interfaces
{
    public interface IInvoiceApp
    {
        Task<Response<NewInvoiceResponse>> Invoice(Request<NewInvoiceRequest> request);
    }
}