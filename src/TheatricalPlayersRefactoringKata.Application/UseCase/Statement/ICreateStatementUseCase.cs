using TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Create;

namespace TheatricalPlayersRefactoringKata.Application.UseCase.Statement;

public interface ICreateStatementUseCase
{
    CreateStatementOutput Execute(CreateStatementInput input);
}