using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces;

public interface IStatementFormatter
{
    string Print(StatementResult statement);
}
