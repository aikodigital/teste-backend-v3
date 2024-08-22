using System;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class ComedyCalculator : ICalculator
    {
        public int CalculateAmount(Performance performance)
        {
            int amount = 30000;
            if (performance.Audience > 20)
            {
                amount += 10000 + 500 * (performance.Audience - 20);
            }
            return amount;
        }

        public int CalculateCredits(Performance performance)
        {
            return (int)Math.Floor(performance.Audience / 5.0);
        }
    }
}
