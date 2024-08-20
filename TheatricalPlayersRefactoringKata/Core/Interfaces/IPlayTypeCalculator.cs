using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces
{
    public interface IPlayTypeCalculator
    {
        bool CanHandle(string playType);
        decimal CalculateAmount(Play play, Performance performance);
        int CalculateCredits(Play play, Performance performance);
    }
}