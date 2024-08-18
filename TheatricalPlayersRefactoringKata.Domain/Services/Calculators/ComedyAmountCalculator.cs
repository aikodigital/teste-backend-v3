using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Services.Calculators
{
    public static class ComedyAmountCalculator
    {
        public static int Calculate(Performance perf, Play play, int baseAmount)
        {
            if (perf.Audience > 20)
            {
                baseAmount += 10000 + 500 * (perf.Audience - 20);
            }
            baseAmount += 300 * perf.Audience;

            return baseAmount;
        }
    }
}