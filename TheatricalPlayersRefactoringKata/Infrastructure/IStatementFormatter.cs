using TheatricalPlayersRefactoringKata.Data;

namespace TheatricalPlayersRefactoringKata.Infrastructure
{
    public interface IStatementFormatter
    {
        string Format(StatementData statementData);
    }
}
