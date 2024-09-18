using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class TragedyStrategy : PlayStrategyBase
    {
        public override int Calculate(Performance performance, Play play)
        {
            var lines = AdjustLines(play);
            var thisAmount = lines * 10;

            if (performance.Audience > 30)
            {
                thisAmount += 1000 * (performance.Audience - 30);
            }

            return thisAmount;
        }
    }

}
