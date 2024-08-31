using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Gender;

public class HistoryCalculator : ICalculatorFactory
{
    public static int CalculateAmount(Performance perf, Play play)
    {
        return TragedyCalculator.CalculateAmount(perf, play) + ComedyCalculator.CalculateAmount(perf, play);
    }

}
