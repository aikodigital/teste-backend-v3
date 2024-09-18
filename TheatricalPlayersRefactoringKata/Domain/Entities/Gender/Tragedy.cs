using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Domain.Entities.Gender
{
    public class Tragedy : IGender
    {
        private const int AudienceLimit = 30;
        private const decimal AdditionalChargePerExtraAudience = 10m;
        public decimal Calculate(decimal basePrice, int audience)
        {
            decimal totalCost = basePrice;

            if (audience > AudienceLimit)
            {
                int extraAudience = audience - AudienceLimit;
                totalCost += AdditionalChargePerExtraAudience * extraAudience;
            }

            return totalCost;
        }
    }
}
