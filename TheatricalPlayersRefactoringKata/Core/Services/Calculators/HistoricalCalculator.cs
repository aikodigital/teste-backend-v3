#region

using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

#endregion

namespace TheatricalPlayersRefactoringKata.Core.Services.Calculators;

public abstract class HistoricalCalculator : ICalculator
{
    public static int CalculateAmount(Performance perf, Play? play)
    {
        return TragedyCalculator.CalculateAmount(perf, play) + ComedyCalculator.CalculateAmount(perf, play);
    }
}