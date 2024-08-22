using System;
using TheatricalPlayersRefactoringKata.Core;

namespace TheatricalPlayersRefactoringKata
{
    public class HistoryCalculator : ICalculator
    {
        public int CalculateAmount(Performance performance, Play play)
        {
            int thisAmount = play.Lines * 10;
            if (performance.Audience > 30)
            {
                thisAmount += 500 * (performance.Audience - 30);
            }
            return thisAmount;
        }

        public int CalculateCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
