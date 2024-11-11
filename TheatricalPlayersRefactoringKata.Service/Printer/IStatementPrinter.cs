using TheatricalPlayersRefactoringKata.Model.Models;

namespace TheatricalPlayersRefactoringKata.Service.Printer;

public interface IStatementPrinter
{
    string Print(Invoice invoice, Dictionary<string, Play> plays);
}
