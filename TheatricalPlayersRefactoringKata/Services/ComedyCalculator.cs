using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class ComedyCalculator : IPerformanceCalculator
    {
        public int CalculateAmount(Performance performance, Play play)
        {
            var lines = Math.Clamp(play.Lines, 1000, 4000);
            int amount = lines * 10;
            if (performance.Audience > 20)
            {
                amount += 10000 + 500 * (performance.Audience - 20);
            }
            amount += 300 * performance.Audience;
            return amount;
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            int credits = Math.Max(performance.Audience - 30, 0);
            credits += (int)Math.Floor((decimal)performance.Audience / 5);
            return credits;
        }
    }
}
