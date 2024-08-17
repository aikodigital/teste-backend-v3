using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    internal class CollectionChargy : IChargeStrategy
    {
        public int CalculateBilling(Performance performance, Play play)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            var amount = lines * 10;

            if (performance.Audience > 20)
            {
                amount += 10000 + 500 * (performance.Audience - 20);
            }
            amount += 300 * performance.Audience;

            return amount;
        }

        public int CalculateCredits(Performance performance)
        {
            var credits = Math.Max(performance.Audience - 30, 0);
            credits += (int)Math.Floor((decimal)performance.Audience / 5);
            return credits;
        }
    }
}
