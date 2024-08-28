using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class TragedyCalculator : IPerformanceCalculator
    {
        public int CalculateAmount(Performance performance, Play play)
        {
            var lines = Math.Clamp(play.Lines, 1000, 4000);
            int amount = lines * 10;
            if (performance.Audience > 30)
            {
                amount += 1000 * (performance.Audience - 30);
            }
            return amount;
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
