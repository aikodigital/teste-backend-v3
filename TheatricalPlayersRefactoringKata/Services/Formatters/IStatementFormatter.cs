using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services.Formatters;

public interface IStatementFormatter
{
    string Format(Statement statement);
}
