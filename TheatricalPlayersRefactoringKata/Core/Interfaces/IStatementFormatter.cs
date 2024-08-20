using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IStatementFormatter
{
    string Format(Invoice invoice, Dictionary<string, Play> plays);
}

