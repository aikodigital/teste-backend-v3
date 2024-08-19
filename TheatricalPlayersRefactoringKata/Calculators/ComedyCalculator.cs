using System;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class ComedyCalculator : IPlayCalculator
    {
        public decimal CalculateAmount(Play play, int audience)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            else if (lines > 4000) lines = 4000;

            decimal baseAmount = (lines / 10) + 3 * audience;
            if (audience > 20)
            {
                baseAmount += 100 + 5 * (audience - 20);
            }
            return baseAmount;
        }

        public int CalculateCredits(Play play, int audience)
        {
            var credits = Math.Max(audience - 30, 0);
            credits += (int)Math.Floor((decimal)audience / 5);
            return credits;
        }
    }
}