using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.Strategies
{
    internal class HistoryPlayTypeStrategies : IPlayTypeStrategy
    {
        public int CalculateTotalAmountByAudience(int baseValue, int audience)
        {
            var comedyCalcAmountStrategy = new ComedyPlayTypeStrategies();
            var tragedyCalcAmoutStrategy = new TragedyPlayTypeStrategies();

            int totalAmount = 0;

            totalAmount += comedyCalcAmountStrategy.CalculateTotalAmountByAudience(baseValue, audience);
            totalAmount += tragedyCalcAmoutStrategy.CalculateTotalAmountByAudience(baseValue, audience);

            return totalAmount;
        }

        public int CalculateCreditsByAudience(int audience)
        {
            return Math.Max(audience - 30, 0);
        }
    }
}
