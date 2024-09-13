using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Calculator
{
    public class ComedyAmountCalculator : IPlayAmountCalculator
    {
        public int CalculateAmount(Performance perf, int baseAmount)
        {
            if (perf.Audience > 20)
            {
                baseAmount += 10000 + 500 * (perf.Audience - 20);
            }
            baseAmount += 300 * perf.Audience;

            return baseAmount;
        }

        public int CalculateEarnedCredits(int audience)
        {
            int volumeCredits = Math.Max(audience - 30, 0);
            volumeCredits += (int)Math.Floor((decimal)audience / 5);

            return volumeCredits;
        }
    }
}
