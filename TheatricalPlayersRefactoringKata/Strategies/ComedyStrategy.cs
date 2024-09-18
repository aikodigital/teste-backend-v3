using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class ComedyStrategy : PlayStrategyBase
    {
        public override int Calculate(Performance performance, Play play)
        {
            var lines = AdjustLines(play);
            var thisAmount = lines * 10;

            if (performance.Audience > 20)
            {
                thisAmount += 10000 + 500 * (performance.Audience - 20);
            }
            thisAmount += 300 * performance.Audience;

            return thisAmount;
        }
    }
}
