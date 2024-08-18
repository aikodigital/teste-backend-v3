using System;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class TragedyCalculator : IPlayCalculator
    {
        public decimal CalculateAmount(Play play, Performance perf)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            var baseAmount = lines / 10.0m;
            if (perf.Audience > 30)
            {
                baseAmount += 10 * (perf.Audience - 30);
            }
            return Math.Round(baseAmount, 2);
        }

        public int CalculateVolumeCredits(Play play, Performance perf)
        {
            return Math.Max(perf.Audience - 30, 0);
        }
    }
}
