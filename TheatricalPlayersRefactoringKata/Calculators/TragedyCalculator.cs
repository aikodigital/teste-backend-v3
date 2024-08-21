using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators;

public class TragedyCalculator : IPlayCalculator
{
    public double CalculateAmount(Performance performance, Play play)
    {
        double baseAmount = Math.Clamp(play.Lines, 1000, 4000) * 10;
        double result = baseAmount;
        if (performance.Audience > 30)
        {
            result += 1000 * (performance.Audience - 30);
        }
        return result;
    }
     public int CalculateCredits(Performance performance)
    {
       return Math.Max(performance.Audience - 30, 0);
    }
}