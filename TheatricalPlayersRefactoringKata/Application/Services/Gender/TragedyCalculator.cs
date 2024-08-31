using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Gender;

public class TragedyCalculator : ICalculatorFactory
{
    public static int CalculateAmount(Performance perf, Play play)
    {
        return perf.Audience > 30
            ? 1000 * (perf.Audience - 30) + ICalculatorFactory.DefaultAmount(play.Lines)
            : ICalculatorFactory.DefaultAmount(play!.Lines);
    }
}
