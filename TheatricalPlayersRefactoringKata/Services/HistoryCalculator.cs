using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class HistoryCalculator : IPerformanceCalculator
    {
        private readonly TragedyCalculator _tragedyCalculator = new TragedyCalculator();
        private readonly ComedyCalculator _comedyCalculator = new ComedyCalculator();

        public int CalculateAmount(Performance performance, Play play)
        {
            return _tragedyCalculator.CalculateAmount(performance, play) +
                   _comedyCalculator.CalculateAmount(performance, play);
        }

        public int CalculateVolumeCredits(Performance performance, Play play)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
