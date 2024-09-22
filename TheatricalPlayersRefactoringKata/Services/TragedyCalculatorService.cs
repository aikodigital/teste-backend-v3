using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class TragedyCalculatorService : ICalculator
{
    public int CalculateAmount(Performance performance, Play play)
    {
        var baseAmount = play.Lines < 1000 ? 1000 : (play.Lines > 4000 ? 4000 : play.Lines) * 10;
        if (performance.Audience > 30)
        {
            baseAmount += 1000 * (performance.Audience - 30);
        }
        return baseAmount;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        return Math.Max(performance.Audience - 30, 0);
    }
}
