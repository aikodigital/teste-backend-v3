using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class Tragedy : IPlay
    {
        public double CalculateAmount(Play play, Performance performance)
        {
            var lines = Math.Clamp(play.Lines, 1000, 4000);
            var baseAmount = lines / 10.0;
            if (performance.Audience > 30)
            {
                baseAmount += 10.0 * (performance.Audience - 30);
            }
            return baseAmount;
        }

        public int CalculateVolumeCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
