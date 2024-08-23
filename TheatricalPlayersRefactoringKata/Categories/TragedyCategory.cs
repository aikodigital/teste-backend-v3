using System;

namespace TheatricalPlayersRefactoringKata.Categories
{
    public class TragedyCategory : IPlayCategory
    {
        public decimal CalculateAmount(int seats, int performanceId)
        {
            // Valor base para tragédia
            decimal baseAmount = Math.Max(1000, Math.Min(4000, seats)) / 10m;
            if (seats <= 30)
            {
                return baseAmount;
            }
            else
            {
                return baseAmount + (seats - 30) * 10.00m;
            }
        }

        public int CalculatePoints(int seats)
        {
            return (seats > 30) ? (seats - 30) : 0;
        }
    }
}
