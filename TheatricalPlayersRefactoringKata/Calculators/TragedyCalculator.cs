using System;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Core;

namespace TheatricalPlayersRefactoringKata{
    public class TragedyCalculator : IPlayTypeCalculator
    {
        public decimal CalculateAmount(Performance perf, Play play)
        {
            decimal thisAmount = Math.Max(1000, Math.Min(play.Lines, 4000)) * 10;
            if (perf.Audience > 30)
            {
                thisAmount += 1000m * (perf.Audience - 30);
            }
            return thisAmount;
        }
        public decimal CalculateVolumeCredits(Performance perf)
        {
            return Math.Max(perf.Audience - 30, 0);
        }
    }
}
