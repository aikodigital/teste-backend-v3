using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public interface ICalculator
    {
        int CalculateAmount(Performance performance);
        int CalculateCredits(Performance performance);
    }
}
