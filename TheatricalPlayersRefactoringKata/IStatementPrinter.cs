using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata
{
    public interface IStatementPrinter
    {
        string Print(Invoice invoice, Dictionary<string, Play> plays);
    }
}
