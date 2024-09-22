using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class TragedyCalculatorService : CalculatorService, ICalculator
{
    public int CalculateAmount(Performance performance, Play play)
    {
        var amount = CalculateBaseAmount(play.Lines);
        
        if (performance.Audience > 30)
        {
            amount += 1000 * (performance.Audience - 30);
        }

        return amount;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        return Math.Max(performance.Audience - 30, 0);
    }
}
