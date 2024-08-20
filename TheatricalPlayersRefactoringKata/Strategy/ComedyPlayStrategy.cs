using System;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class ComedyPlayStrategy : IPlayStrategy
    {
        public double CalculateAmount(int audience, int lines)
        {
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            
            double amount = lines * 10;
            if (audience > 20) {
                amount += 10000.0 + 500.0 * (audience - 20);
            }
            amount += 300.0 * audience;
            return amount;
        }

        public int CalculateCredits(int audience)
        {
            int credits = Math.Max(audience - 30, 0);
            credits += (int)Math.Floor((decimal)audience / 5);
            return credits;
        }
    }
}
