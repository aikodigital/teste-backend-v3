using System;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class TragedyStrategy : IPlayTypeStrategy
    {
        public decimal CalculateAmount(int lines, int audience)
        {
            int validLines = Math.Clamp(lines, 1000, 4000);
            decimal baseAmount = validLines / 10m;

            if (audience > 30)
            {
                baseAmount += 10m * (audience - 30);
            }
            return baseAmount;
        }

        public int CalculateVolumeCredits(int audience)
        {
            return (audience > 30) ? audience - 30 : 0;
        }
    }
}
