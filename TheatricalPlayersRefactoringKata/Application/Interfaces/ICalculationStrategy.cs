using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces;

public interface ICalculationStrategy
{
    decimal CalculateAmount(Performance perf, Play play);
    decimal CalculateCredits(Performance perf, Play play);
}
