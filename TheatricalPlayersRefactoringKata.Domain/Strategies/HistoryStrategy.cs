using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Utilities;
using TheatricalPlayersRefactoringKata.Infrastructure.Utilities;

namespace TheatricalPlayersRefactoringKata.Domain.Strategies
{
    public class HistoryStrategy: IGenreStrategy
    {
        private readonly TragedyStrategy _tragedy = new ();
        private readonly ComedyStrategy _comedy = new ();

        public decimal CalculateCost(int audienceSize, int lines)
        {
            //As peças históricas são, por algum motivo, mais complicadas e têm o valor igual à soma dos valores correspondentes a uma peça de tragédia e uma de comédia
            return _tragedy.CalculateCost(audienceSize, lines)
                 + _comedy.CalculateCost(audienceSize, lines);
        }

        public decimal CalculateBasePrice(int lines)
        {
            return PricingHelper.CalculateBasePrice(lines);
        }
        public int CalculateCredits(int audienceSize) => CreditHelper.CalculateBaseCredit(audienceSize);
    }
}
