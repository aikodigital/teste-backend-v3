using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    internal interface IStatementFormatter
    {
        string Format(Invoice invoice, Dictionary<string, Play> plays);
    }
}
