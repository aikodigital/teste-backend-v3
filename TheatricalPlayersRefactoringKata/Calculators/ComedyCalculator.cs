using System;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Core;

namespace TheatricalPlayersRefactoringKata
{
    public class ComedyCalculator : IPlayTypeCalculator
    {
        public decimal CalculateAmount(Performance perf, Play play)
        {
            decimal thisAmount = Math.Max(1000, Math.Min(play.Lines, 4000)) * 10;
            if (perf.Audience > 20)
            {
                thisAmount += 10000m + 500m * (perf.Audience - 20);
            }
            thisAmount += 300m * perf.Audience;
            return thisAmount;
        }

        public decimal CalculateVolumeCredits(Performance perf)
        {
            return Math.Max(perf.Audience - 30, 0) + (int)Math.Floor((decimal)perf.Audience / 5);
        }
    }
}
