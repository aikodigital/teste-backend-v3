using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Services.Calculators
{
    public class HistoryAmountCalculator
    {
        public static int Calculate(Performance perf, Play play, int baseAmount)
        {
            var tragedyAmount = TragedyAmountCalculator.Calculate(perf, play, baseAmount);
            var comedyAmount = ComedyAmountCalculator.Calculate(perf, play, baseAmount);

            return tragedyAmount + comedyAmount;
        }
    }
}