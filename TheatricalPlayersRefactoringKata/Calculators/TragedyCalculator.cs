using System;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class TragedyCalculator : IPlayCalculator
    {
        public decimal CalculateAmount(Play play, int audience)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            decimal baseAmount = lines / 10m;
            if (audience > 30)
            {
                baseAmount += 10 * (audience - 30);
            }
            return Math.Round(baseAmount, 2);
        }

        public int CalculateCredits(Play play, int audience) => Math.Max(audience - 30, 0);
    }
}