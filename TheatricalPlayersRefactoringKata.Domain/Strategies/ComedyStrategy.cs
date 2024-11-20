using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Utilities;

namespace TheatricalPlayersRefactoringKata.Domain.Strategies
{
    public class ComedyStrategy : IGenreStrategy
    {
        public decimal CalculateCost(int audienceSize, int lines)
        {

            //Para uma peça de comédia, o cálculo base é sempre somado a 3.00 por espectador. Além disso, se a platéia for maior que 20, o valor deve ser aumentado em 100.00 e deve se somar mais 5.00 por espectador adicional aos 20 de base
            var basePrice = PricingHelper.CalculateBasePrice(lines);
            decimal additionalCost = 3.0m * audienceSize;
            if (audienceSize > 20)
            {
                additionalCost += 100.0m + 5.0m * (audienceSize - 20);
            }
            return basePrice + additionalCost;
        }

        public int CalculateCredits(int audienceSize)
        {
            //Existe um bônus de créditos de um quinto da platéia arredondados para baixo, exclusivo para peças de comédia
            int bonusCredits = (int)Math.Floor(audienceSize / 5.0);
            return Math.Max(0, audienceSize - 30) + bonusCredits;
        }
    }
}
