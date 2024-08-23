#region

using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

#endregion

namespace TheatricalPlayersRefactoringKata.Core.Services.Calculators;

public abstract class TragedyCalculator : ICalculator
{
    public static int CalculateAmount(Performance perf, Play play)
    {
        return perf.Audience > 30
            ? 1000 * (perf.Audience - 30) + ICalculator.DefaultAmount(play.Lines)
            : ICalculator.DefaultAmount(play!.Lines);
    }
}