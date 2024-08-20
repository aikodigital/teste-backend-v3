using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class Comedy : IPlay
    {
        public double CalculateAmount(Play play, Performance performance)
        {
            var lines = Math.Clamp(play.Lines, 1000, 4000);
            var baseAmount = lines / 10.0;
            var amount = baseAmount + 3.0 * performance.Audience;
            if (performance.Audience > 20)
            {
                amount += 100.0 + 5.0 * (performance.Audience - 20);
            }
            return amount;
        }

        public int CalculateVolumeCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0) + (int)Math.Floor((decimal)performance.Audience / 5);
        }
    }

}
