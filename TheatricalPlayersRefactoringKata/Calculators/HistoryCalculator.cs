using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators;

public class HistoryCalculator : IPlayCalculator
{
    public double CalculateAmount(Performance performance, Play play)
    {
        var tragedyCalculator = new TragedyCalculator();
        var comedyCalculator = new ComedyCalculator();

        double tragedyAmount = tragedyCalculator.CalculateAmount(performance, play);
        double comedyAmount = comedyCalculator.CalculateAmount(performance, play);

        return tragedyAmount + comedyAmount;
    }
        public int CalculateCredits(Performance performance)
    {
        return Math.Max(performance.Audience - 30, 0);
    }
}