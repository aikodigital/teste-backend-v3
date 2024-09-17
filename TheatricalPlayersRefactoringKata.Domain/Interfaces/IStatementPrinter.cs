using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces
{
    public interface IStatementPrinter
    {
        string Print(Invoice invoice, Dictionary<string, Play> plays);
    }
}
