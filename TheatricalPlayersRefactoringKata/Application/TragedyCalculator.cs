using System;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Services
{
    public class TragedyCalculator : IPerformanceCalculator
    {
        public bool CanHandle(string genre) => genre == Genre.Tragedy.ToString();

        public decimal CalculatePrice(Performance performance)
        {
            decimal basePrice = Math.Max(1000, Math.Min(4000, performance.Lines)) / 10m;

            if (performance.Audience > 30)
            {
                basePrice += 10 * (performance.Audience - 30);
            }

            return basePrice;
        }

        public int CalculateCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}