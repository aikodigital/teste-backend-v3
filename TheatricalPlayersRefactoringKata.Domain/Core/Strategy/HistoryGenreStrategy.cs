using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Core.Strategy {
    public class HistoryGenreStrategy : IGenreStrategy {

        private readonly TragedyGenreStrategy _tragedyGenreObj = new();
        private readonly ComedyGenreStrategy _comedyGenreObj = new();

        public int CalculatePlayCredits(Performance perf) {
            int volumeCredits = Math.Max(perf.Audience - 30, 0);

            return volumeCredits;

        }

        public double CalculatePlayAmount(Performance perf, double thisAmount) {
            double totalAmount = 0;

            double tragedyAmount = _tragedyGenreObj.CalculatePlayAmount(perf, thisAmount);
            double comedyAmount = _comedyGenreObj.CalculatePlayAmount(perf, thisAmount);

            totalAmount = tragedyAmount + comedyAmount;

            return totalAmount;
        }
    }
}
