using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class VolumeCreditsCalculator
    {
        public int Calculate(Performance perf, Play play)
        {
            var volumeCredits = Math.Max(perf.Audience - 30, 0);

            // add extra credit for every ten comedy attendees
            if (play.Type == "comedy")
            {
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            }

            return volumeCredits;
        }
    }
}
