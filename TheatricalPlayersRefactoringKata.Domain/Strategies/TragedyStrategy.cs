using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Utilities;
using TheatricalPlayersRefactoringKata.Infrastructure.Utilities;

namespace TheatricalPlayersRefactoringKata.Domain.Strategies
{
    public class TragedyStrategy : IGenreStrategy
    {
        public decimal CalculateCost(int audienceSize, int lines)
        {
            //O valor para uma peça de tragédia é igual ao valor base caso a platéia seja menor ou igual a 30, somando mais 10.00 para cada espectador adicional a esses 30
            var basePrice = CalculateBasePrice(lines);
            return audienceSize <= 30
                ? basePrice
                : basePrice + 10.0m * (audienceSize - 30);
        }
        public decimal CalculateBasePrice(int lines)
        {
            return PricingHelper.CalculateBasePrice(lines);
        }
        public int CalculateCredits(int audienceSize) => CreditHelper.CalculateBaseCredit(audienceSize);
    }
}
