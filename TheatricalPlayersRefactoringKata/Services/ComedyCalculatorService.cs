using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class ComedyCalculatorService : ICalculator
{
    public int CalculateAmount(Performance performance, Play play)
    {
        var baseAmount = play.Lines < 1000 ? 1000 : (play.Lines > 4000 ? 4000 : play.Lines) * 10;
        if (performance.Audience > 20)
        {
            baseAmount += 10000 + 500 * (performance.Audience - 20);
        }
        baseAmount += 300 * performance.Audience;
        return baseAmount;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        return Math.Max(performance.Audience - 30, 0) + (int)Math.Floor((decimal)performance.Audience / 5);
    }
}
