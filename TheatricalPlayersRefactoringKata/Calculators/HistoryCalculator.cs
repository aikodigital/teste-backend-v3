using System;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class HistoryCalculator : IPlayCalculator
    {
        private readonly TragedyCalculator _tragedyCalculator = new TragedyCalculator();
        private readonly ComedyCalculator _comedyCalculator = new ComedyCalculator();

        public decimal CalculateAmount(Play play, Performance perf)
        {
            var tragedyAmount = _tragedyCalculator.CalculateAmount(play, perf);
            var comedyAmount = _comedyCalculator.CalculateAmount(play, perf);

            return Math.Round(tragedyAmount + comedyAmount, 2);
        }

        public int CalculateVolumeCredits(Play play, Performance perf)
        {
            return Math.Max(perf.Audience - 30, 0); ;
        }
    }
}
