using System;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class TragedyPlayStrategy : IPlayStrategy
    {
        public double CalculateAmount(int audience, int lines)
        {
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            double amount = lines * 10;
            if (audience > 30) {
                amount += 1000.0 * (audience - 30);
            }
            return amount;
        }

        public int CalculateCredits(int audience)
        {
             return Math.Max(audience - 30, 0);
        }
    }
}
