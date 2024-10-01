using TheatricalPlayersRefactoringKata.Core;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class HistoryCalculator : IPlayTypeCalculator
    {
        public decimal CalculateAmount(Performance performance, Play play)
        {
            var tragedyCalc = new TragedyCalculator();
            var comedyCalc = new ComedyCalculator();

            decimal tragedyAmount = tragedyCalc.CalculateAmount(performance, play);
            decimal comedyAmount = comedyCalc.CalculateAmount(performance, play);

            return tragedyAmount + comedyAmount;
        }
        public decimal CalculateVolumeCredits(Performance performance)
        {
            return new TragedyCalculator().CalculateVolumeCredits(performance);
        }
    }
}
