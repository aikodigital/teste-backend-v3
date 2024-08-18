using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Services.Calculators
{
    public static class TragedyAmountCalculator
    {
        public static int Calculate(Performance perf, Play play, int baseAmount)
        {
            if (perf.Audience > 30)
            {
                baseAmount += 1000 * (perf.Audience - 30);
            }
            return baseAmount;
        }

    }
}