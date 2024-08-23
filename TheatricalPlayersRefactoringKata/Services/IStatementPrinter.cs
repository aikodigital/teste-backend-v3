using TheatricalPlayersRefactoringKata.Models;
using System.Collections.Generic;

public interface IStatementPrinter
{
    string Print(Invoice invoice, Dictionary<string, Play> plays);
}
