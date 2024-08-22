using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Services.Calculators
{
    public static class ComedyAmountCalculator
    {
        public static int Calculate(Performance perf, Play play)
        {
            var amount = play.CalculateBaseAmount();
            if (perf.Audience > 20)
            {
                amount += 10000 + 500 * (perf.Audience - 20);
            }
            amount += 300 * perf.Audience;

            return amount;
        }
    }
}