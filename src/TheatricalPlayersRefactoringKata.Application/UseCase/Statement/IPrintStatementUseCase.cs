using TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Print;

namespace TheatricalPlayersRefactoringKata.Application.UseCase.Statement;

public interface IPrintStatementUseCase
{
    string Execute(PrintStatementInput input);
}