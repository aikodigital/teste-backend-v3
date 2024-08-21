using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Utilities;

public interface IStatementFormatter
{
    string FormatStatement(Invoice invoice, Dictionary<string, Play> plays);
}