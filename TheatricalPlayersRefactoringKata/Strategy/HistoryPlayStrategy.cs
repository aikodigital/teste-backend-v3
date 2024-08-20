using System;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class HistoryPlayStrategy : IPlayStrategy
    {
        public double CalculateAmount(int audience, int lines)
        {
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            double tragedyAmount = lines * 10;
            if (audience > 30) {
                tragedyAmount += 1000.0 * (audience - 30);
            }

            int comedyAmount = lines * 10;
            if (audience > 20) {
                comedyAmount += 10000 + 500 * (audience - 20);
            }
            comedyAmount += 300 * audience;


            return tragedyAmount + comedyAmount;
        }

        public int CalculateCredits(int audience)
        {
            int credits = Math.Max(audience - 30, 0);
            return credits;
        }
    }
}
