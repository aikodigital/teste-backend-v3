using System;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class TragedyStrategy : IStatementStrategy
    {
        public decimal CalculatePrice(Play play, Performance perf)
        {
            if (perf.Audience <= 0)
                return 0;

            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10;

            if (perf.Audience > 30)
            {
                thisAmount += 1000 * (perf.Audience - 30);
            }

            return thisAmount;
        }

        public int CalculateCredits(Play play, Performance perf)
        {
            return Math.Max(perf.Audience - 30, 0);
        }
    }
}
