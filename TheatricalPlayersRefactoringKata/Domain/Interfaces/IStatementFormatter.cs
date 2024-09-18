using TheatricalPlayersRefactoringKata.Application.DTOs;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces;

public interface IStatementFormatter
{
    string Print(StatementResult statement);
}
