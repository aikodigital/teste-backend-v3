using System;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Gender;

public class ComedyCalculator : ICalculatorFactory
{
    public static int CalculateAmount(Performance perf, Play play)
    {
        var baseAmount = 300 * perf.Audience + ICalculatorFactory.DefaultAmount(play.Lines);
        return perf.Audience > 20
            ? 10000 + 500 * (perf.Audience - 20) + baseAmount
            : baseAmount;
    }
}

