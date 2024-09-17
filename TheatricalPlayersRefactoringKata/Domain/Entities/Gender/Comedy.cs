using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Domain.Entities.Gender
{
    public class Comedy : IGender
    {
        private const int AudienceLimit = 20;
        private const decimal AdditionalChargePerExtraAudience = 5m;
        private const decimal BaseBonus = 100m;
        private const decimal ChargePerAudienceMember = 3m;

        public decimal Calculate(decimal basePrice, int audience)
        {
            decimal totalCost = basePrice + ChargePerAudienceMember * audience;

            if (audience > AudienceLimit)
            {
                int extraAudience = audience - AudienceLimit;
                totalCost += BaseBonus + AdditionalChargePerExtraAudience * extraAudience;
            }

            return totalCost;
        }
    }
}
