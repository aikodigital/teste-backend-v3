using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.Validations.Invoices.Register;
public interface IRegisterInvoiceValidation
{
    Task<ResponseInvoice> Execute(RequestInvoice request);
}
