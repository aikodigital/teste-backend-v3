using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators;

public interface IPlayCalculator
{
    double CalculateAmount(Performance performance, Play play);
    int CalculateCredits(Performance performance);
}