using System;
using TheatricalPlayersRefactoringKata.Calculators.Interface;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators;

public class TragedyPlayCalculator : ICalculator
{
    public decimal CalculateAmount(Performance performance, Play play)
    {
        decimal totalAmount = 0;
        var lines = play.Lines;

        if (lines < 1000)
            lines = 1000;
        if (lines > 4000)
            lines = 4000;

        var thisAmount = lines * 10;

        if (performance.Audience > 30)
            thisAmount += 1000 * (performance.Audience - 30);

        totalAmount = thisAmount;
        return totalAmount;
    }

    public int CalculateCredits(Performance performance)
    {
        var volumeCredits = 0;
        return volumeCredits += Math.Max(performance.Audience - 30, 0);
    }
}
