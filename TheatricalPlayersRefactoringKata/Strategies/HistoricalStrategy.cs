using System;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class HistoricalStrategy : IStatementStrategy
    {
        private readonly TragedyStrategy _tragedyStrategy = new TragedyStrategy();
        private readonly ComedyStrategy _comedyStrategy = new ComedyStrategy();

        public decimal CalculatePrice(Play play, Performance perf)
        {
            var tragedyPrice = _tragedyStrategy.CalculatePrice(play, perf);
            var comedyPrice  = _comedyStrategy.CalculatePrice(play, perf);

             return tragedyPrice + comedyPrice;
        }

        public int CalculateCredits(Play play, Performance perf)
        {
            return Math.Max(perf.Audience - 30, 0);
        }
    }
}
