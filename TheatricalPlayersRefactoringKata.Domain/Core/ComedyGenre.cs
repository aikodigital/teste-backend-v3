using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Core {
    public class ComedyGenre : IPlayGenre {

        public int CalculatePlayCredits(Performance perf) {
            int volumeCredits = Math.Max(perf.Audience - 30, 0);

            // add extra credit for every ten comedy attendees
            volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            return volumeCredits;
        }

        public double CalculatePlayAmount(Performance perf) {
            double thisAmount = 0;

            if (perf.Audience > 20) {
                thisAmount += 10000 + 500 * (perf.Audience - 20);
            }
            thisAmount += 300 * perf.Audience;

            return thisAmount;
        }
    }
}
