using System;
using TheatricalPlayersRefactoringKata.Core;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class ComedyCalculator : ICalculator
    {
        public int CalculateAmount(Performance performance, Play play)
        {
            int thisAmount = play.Lines * 10;
            if (performance.Audience > 20)
            {
                thisAmount += 10000 + 500 * (performance.Audience - 20);
            }
            thisAmount += 300 * performance.Audience;
            return thisAmount;
        }

        public int CalculateCredits(Performance performance)
        {
            int credits = Math.Max(performance.Audience - 30, 0);
            credits += (int)Math.Floor((decimal)performance.Audience / 5);
            return credits;
        }
    }
}
