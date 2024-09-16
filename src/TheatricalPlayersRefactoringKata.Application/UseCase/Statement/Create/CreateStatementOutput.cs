using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Create;

public class CreateStatementOutput
{
    public StatementEntity Statement { get; set; } = null!;
}