using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Services.Calculators
{
    public static class TragedyAmountCalculator
    {
        public static int Calculate(Performance perf, Play play)
        {
            var amount = play.CalculateBaseAmount();
            if (perf.Audience > 30)
            {
                amount += 1000 * (perf.Audience - 30);
            }
            return amount;
        }

    }
}