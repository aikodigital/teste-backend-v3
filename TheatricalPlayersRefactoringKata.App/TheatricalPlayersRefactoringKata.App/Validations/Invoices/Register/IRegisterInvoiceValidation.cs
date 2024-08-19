using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
public interface IRegisterInvoiceValidation
{
    Task<ResponseInvoice> Execute(RequestInvoice request);
}
