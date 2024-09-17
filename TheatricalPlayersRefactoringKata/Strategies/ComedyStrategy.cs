using System;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class ComedyStrategy : IPlayTypeStrategy
    {
        public decimal CalculateAmount(int lines, int audience)
        {
            int validLines = Math.Clamp(lines, 1000, 4000);
            decimal baseAmount = validLines / 10m;

            baseAmount += 3m * audience;

            if (audience > 20)
            {
                baseAmount += 100m + 5m * (audience - 20);
            }
            return baseAmount;
        }

        public int CalculateVolumeCredits(int audience)
        {
            return (audience > 30) ? (audience - 30) + (audience / 5) : 0;
        }
    }
}
