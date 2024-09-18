using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class HistoryStrategy : IPlayStrategy
    {
        private readonly TragedyStrategy _tragedyStrategy = new TragedyStrategy();
        private readonly ComedyStrategy _comedyStrategy = new ComedyStrategy();

        public int Calculate(Performance performance, Play play)
        {
            // Combines the calculations of tragedy and comedy
            var tragedyAmount = _tragedyStrategy.Calculate(performance, play);
            var comedyAmount = _comedyStrategy.Calculate(performance, play);
            return tragedyAmount + comedyAmount;
        }
    }
}
