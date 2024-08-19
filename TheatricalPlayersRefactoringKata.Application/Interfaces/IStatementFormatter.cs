using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IStatementFormatter
    {
        string Format(Invoice invoice, Dictionary<Guid, Play> plays, Dictionary<Performance, int> performanceAmounts, int volumeCredits, decimal totalAmount);
    }
}