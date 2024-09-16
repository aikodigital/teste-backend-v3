using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Printer
{
    public interface IStatementPrinter
    {
        string Print(Invoice invoice, Dictionary<string, Play> plays);
    }
}
