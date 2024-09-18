using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Interfaces
{
    public interface IStatementFormatter
    {
        string Format(Invoice invoice, Dictionary<string, Play> plays, Dictionary<string, IStatementStrategy> strategies);
    }
}
