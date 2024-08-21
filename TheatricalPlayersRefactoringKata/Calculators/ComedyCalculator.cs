using System;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators;

public class ComedyCalculator : IPlayCalculator
{
    public double CalculateAmount(Performance performance, Play play)
    {
        double baseAmount = Math.Clamp(play.Lines, 1000, 4000) * 10;
        double result = baseAmount;
        if (performance.Audience > 20)
        {
            result += 10000 + 500 * (performance.Audience - 20);
        }
        result += 300 * performance.Audience;
        return result;
    }

       public int CalculateCredits(Performance performance)
    {
        int credits = Math.Max(performance.Audience - 30, 0);
        credits += (int)Math.Floor((decimal)performance.Audience / 5);
        return credits;
    }
}