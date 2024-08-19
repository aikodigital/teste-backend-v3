using System;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Services
{
    public class TragedyCalculator : IPerformanceCalculator
    {
        public decimal CalculatePrice(Performance performance)
        {
            decimal basePrice = Math.Max(1000, Math.Min(4000, performance.Lines)) / 10m;
            basePrice += 3 * performance.Audience;

            if (performance.Audience > 20)
            {
                basePrice += 100 + 5 * (performance.Audience - 20);
            }

            return basePrice;
        }

        public int CalculateCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}