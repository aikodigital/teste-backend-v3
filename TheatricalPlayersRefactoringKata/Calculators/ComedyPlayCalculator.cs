using System;
using TheatricalPlayersRefactoringKata.Calculators.Interface;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators;

public class ComedyPlayCalculator : ICalculator
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

        if (performance.Audience > 20)
            thisAmount += 10000 + 500 * (performance.Audience - 20);

        thisAmount += 300 * performance.Audience;

        return totalAmount = thisAmount;
    }

    public int CalculateCredits(Performance performance)
    {
        var volumeCredits = 0;
        volumeCredits += Math.Max(performance.Audience - 30, 0);
        volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);
        return volumeCredits;
        
    }
}

