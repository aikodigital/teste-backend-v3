using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Services.Calculators
{
    public class HistoryAmountCalculator
    {
        public static int Calculate(Performance perf, Play play)
        {
            var tragedyAmount = TragedyAmountCalculator.Calculate(perf, play);
            var comedyAmount = ComedyAmountCalculator.Calculate(perf, play);

            return tragedyAmount + comedyAmount;
        }
    }
}