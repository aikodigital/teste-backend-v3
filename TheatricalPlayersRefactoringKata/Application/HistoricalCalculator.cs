using System;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Services
{
    public class HistoricalCalculator : IPerformanceCalculator
    {
        private readonly IPerformanceCalculator _tragedyCalculator = new TragedyCalculator();
        private readonly IPerformanceCalculator _comedyCalculator = new ComedyCalculator();

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