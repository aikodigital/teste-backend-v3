using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Interfaces
{
    public interface IPlayTypeCalculator
    {
        decimal CalculateAmount(Play play, Performance performance);
        int CalculateCredits(Performance performance);
    }
}
