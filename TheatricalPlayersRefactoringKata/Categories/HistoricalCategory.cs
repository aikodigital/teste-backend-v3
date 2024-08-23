using System;

namespace TheatricalPlayersRefactoringKata.Categories
{
    public class HistoricalCategory : IPlayCategory
    {
        private readonly IPlayCategory _tragedyCategory = new TragedyCategory();
        private readonly IPlayCategory _comedyCategory = new ComedyCategory();

        public decimal CalculateAmount(int seats, int performanceId)
        {
            decimal tragedyAmount = _tragedyCategory.CalculateAmount(seats, performanceId);
            decimal comedyAmount = _comedyCategory.CalculateAmount(seats, performanceId);
            return tragedyAmount + comedyAmount;
        }

        public int CalculatePoints(int seats)
        {
            return _tragedyCategory.CalculatePoints(seats);
        }
    }
}
