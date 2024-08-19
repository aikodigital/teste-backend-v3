using System;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class HistoryCalculator : IPlayCalculator
    {
        private readonly ComedyCalculator _comedyCalculator = new ComedyCalculator();
        private readonly TragedyCalculator _tragedyCalculator = new TragedyCalculator();

        public decimal CalculateAmount(Play play, int audience) => _tragedyCalculator.CalculateAmount(play, audience) + _comedyCalculator.CalculateAmount(play, audience);
        public int CalculateCredits(Play play, int audience) => Math.Max(audience - 30, 0);

    }
}