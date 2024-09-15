using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.Strategies
{
    internal class ComedyPlayTypeStrategies : IPlayTypeStrategy
    {
        public int CalculateTotalAmountByAudience(int baseValue, int audience)
        {
            int amount = baseValue;
            if (audience > 20)
            {
                amount += 10000 + 500 * (audience - 20);
            }
            amount += 300 * audience;
            return amount;
        }

        public int CalculateCreditsByAudience(int valueBase, int audience)
        {
            int credits = valueBase;
            credits += (int)Math.Floor((decimal)audience / 5);
            return credits;
        }
    }
}
