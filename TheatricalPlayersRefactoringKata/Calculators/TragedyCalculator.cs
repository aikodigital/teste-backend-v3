using System;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class TragedyCalculator : ICalculator
    {
        public int CalculateAmount(Performance performance)
        {
            int amount = 40000;
            if (performance.Audience > 30)
            {
                amount += 1000 * (performance.Audience - 30);
            }
            return amount;
        }

        public int CalculateCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
