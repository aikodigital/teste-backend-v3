using System;
using TheatricalPlayersRefactoringKata.Core;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class HistoryCalculator : ICalculator
    {
        public int CalculateAmount(Performance performance, Play play)
        {
            return play.Lines * 15;
        }

        public int CalculateCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 20, 0);
        }
    }
}
