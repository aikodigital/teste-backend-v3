using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Services.Calculators;

public abstract class ComedyCalculator:ICalculator
{
    public static int CalculateAmount(Performance perf, Play? play)
    {
        var baseAmount = 300 * perf.Audience + ICalculator.DefaultAmount(play.Lines);
        return perf.Audience > 20
            ? 10000 + 500 * (perf.Audience - 20) + baseAmount
            : baseAmount;
        
    }
}