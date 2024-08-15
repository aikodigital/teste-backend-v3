using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
public interface IGetAllInvoiceValidation
{
    Task<ResponseInvoices> Execute();
}
