using System;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class HistoryCalculator : IPlayCalculator
    {
        private readonly ComedyCalculator _comedyCalculator = new ComedyCalculator();
        private readonly TragedyCalculator _tragedyCalculator = new TragedyCalculator();

        public decimal CalculateAmount(Play play, int audience)
        {
            var tragedyAmount = _tragedyCalculator.CalculateAmount(play, audience);
            var comedyAmount = _comedyCalculator.CalculateAmount(play, audience);

            return Math.Round(tragedyAmount + comedyAmount, 2);
        }
        public int CalculateCredits(Play play, int audience) => Math.Max(audience - 30, 0);

    }
}