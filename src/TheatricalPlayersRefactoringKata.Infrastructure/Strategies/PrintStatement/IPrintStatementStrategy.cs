using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Strategies.PrintStatement;

public interface IPrintStatementStrategy
{
    string Print(StatementEntity statement);
}