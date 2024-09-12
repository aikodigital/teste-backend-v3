using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class HistoricalStrategy : IPlayTypeStrategy
    {
        private readonly TragedyStrategy _tragedyStrategy = new TragedyStrategy();
        private readonly ComedyStrategy _comedyStrategy = new ComedyStrategy();

        public decimal CalculateAmount(int lines, int audience)
        {
            decimal tragedyAmount = _tragedyStrategy.CalculateAmount(lines, audience);
            decimal comedyAmount = _comedyStrategy.CalculateAmount(lines, audience);
            return tragedyAmount + comedyAmount;
        }

        public int CalculateVolumeCredits(int audience)
        {
            return (audience > 30) ? audience - 30 : 0;
        }
    }

}
