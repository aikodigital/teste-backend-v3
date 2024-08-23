using System;

namespace TheatricalPlayersRefactoringKata.Categories
{
    public class ComedyCategory : IPlayCategory
    {
        public decimal CalculateAmount(int seats, int performanceId)
        {
            decimal baseAmount = Math.Max(1000, Math.Min(4000, seats)) / 10m;
            decimal amount = baseAmount + (seats * 3.00m);

            if (seats > 20)
            {
                amount += 100.00m + (seats - 20) * 5.00m;
            }

            return amount;
        }

        public int CalculatePoints(int seats)
        {
            int basePoints = (int)Math.Floor((decimal)seats / 3) + (int)Math.Floor((decimal)seats / 5);
            int bonusPoints = (int)Math.Floor((decimal)seats / 5);
            return basePoints + bonusPoints;
        }
    }
}
