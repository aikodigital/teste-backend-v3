using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class HistoryCalculator : ICalculator
    {
        public int CalculateAmount(Performance performance)
        {
            return 40000 + 1000 * (performance.Audience - 30);
        }

        public int CalculateCredits(Performance performance)
        {
            return performance.Audience > 40 ? performance.Audience - 30 : 0;
        }
    }
}
