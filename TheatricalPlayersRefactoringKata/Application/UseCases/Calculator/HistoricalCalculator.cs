using System;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Services
{
    public class HistoricalCalculator : IPerformanceCalculator
    {
        private readonly IPerformanceCalculator _tragedyCalculator;
        private readonly IPerformanceCalculator _comedyCalculator;

        public HistoricalCalculator(
            IPerformanceCalculator tragedyCalculator,
            IPerformanceCalculator comedyCalculator)
        {
            _tragedyCalculator = tragedyCalculator ?? throw new ArgumentNullException(nameof(tragedyCalculator));
            _comedyCalculator = comedyCalculator ?? throw new ArgumentNullException(nameof(comedyCalculator));
        }

        public decimal CalculatePrice(Performance performance)
        {
            return _tragedyCalculator.CalculatePrice(performance) + _comedyCalculator.CalculatePrice(performance);
        }

        public int CalculateCredits(Performance performance)
        {
            return _tragedyCalculator.CalculateCredits(performance) + _comedyCalculator.CalculateCredits(performance);
        }
    }
}