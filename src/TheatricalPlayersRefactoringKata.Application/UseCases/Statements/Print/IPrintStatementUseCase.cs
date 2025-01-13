using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Statements.Print
{
    public interface IPrintStatementUseCase
    {
        Task<ResponsePrintStatementJson> ExecuteAsync(RequestPrintStatementJson request);
    }
}
