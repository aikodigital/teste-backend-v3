using System;

namespace TheatricalPlayersRefactoringKata
{
    public class TragedyAmountCalculator : IPlayAmountCalculator
    {
        public int CalculateAmount(Performance perf, int baseAmount)
        {
            if (perf.Audience > 30)
            {
                baseAmount += 1000 * (perf.Audience - 30);
            }

            return baseAmount;
        }

        public int CalculateEarnedCredits(int audience)
        {
            return Math.Max(audience - 30, 0);
        }
    }
}
