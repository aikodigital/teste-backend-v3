using System;

namespace TheatricalPlayersRefactoringKata.Application.Services.Calculators
{
    public class HistoryCalculator : IGenreCalculator
    {
        public decimal CalculateAmount(Performance perf, Play play)
        {
            decimal thisAmount = 50000; // Base amount for history plays
            if (perf.Audience > 25)
            {
                thisAmount += 1500 * (perf.Audience - 25);
            }
            return thisAmount;
        }

        public int CalculateVolumeCredits(Performance perf)
        {
            return Math.Max(perf.Audience - 25, 0);  // Different credit rule for history
        }
    }
}
