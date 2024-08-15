using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application
{
    public class HistoricalCalculator : IPlayTypeCalculator
    {
        private readonly IEnumerable<IPlayTypeCalculator> _calculators;

        public HistoricalCalculator(IEnumerable<IPlayTypeCalculator> calculators)
        {
            _calculators = calculators;
        }

        public bool CanHandle(string playType)
        {
            return playType == "histórica";
        }

        public decimal CalculateAmount(Play play, Performance performance)
        {
            var amount = 0m;
            foreach (var calculator in _calculators)
            {
                if (calculator.CanHandle(play.Type))
                {
                    amount += calculator.CalculateAmount(play, performance);
                }
            }
            return amount;
        }

        public int CalculateCredits(Play play, Performance performance)
        {
            var credits = 0;
            foreach (var calculator in _calculators)
            {
                if (calculator.CanHandle(play.Type))
                {
                    credits += calculator.CalculateCredits(play, performance);
                }
            }
            return credits;
        }
    }
}