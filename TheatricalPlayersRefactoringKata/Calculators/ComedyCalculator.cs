using System;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class ComedyCalculator : IPlayCalculator
    {
        public decimal CalculateAmount(Play play, Performance perf)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            var baseAmount = lines / 10.0m + 3 * perf.Audience;
            if (perf.Audience > 20)
            {
                baseAmount += 100 + 5 * (perf.Audience - 20);
            }
            return Math.Round(baseAmount, 2);
        }

        public int CalculateVolumeCredits(Play play, Performance perf)
        {
            var credits = Math.Max(perf.Audience - 30, 0);
            credits += (int)Math.Floor((decimal)perf.Audience / 5);
            return credits;
        }
    }
}
