using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Calculator
{
    public interface IPlayAmountCalculator
    {
        int CalculateAmount(Performance perf, int baseAmount);
        int CalculateEarnedCredits(int audience);
    }
}
