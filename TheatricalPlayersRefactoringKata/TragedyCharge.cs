using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    internal class TragedyCharge : IChargeStrategy
    {
        public int CalculateBilling(Performance performance, Play play)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            var amount = lines * 10;

            if (performance.Audience > 30)
            {
                amount += 1000 * (performance.Audience - 30);
            }

            return amount;
        }

        public int CalculateCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}