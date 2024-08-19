using System;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Services
{
    public class ComedyCalculator : IPerformanceCalculator
    {
        public decimal CalculatePrice(Performance performance)
        {
            decimal basePrice = 300;
            basePrice += 3 * performance.Audience;

            if (performance.Audience > 20)
            {
                basePrice += 100 + 5 * (performance.Audience - 20);
            }

            return basePrice;
        }

        public int CalculateCredits(Performance performance)
        {
            int credits = Math.Max(performance.Audience - 30, 0);
            credits += (int)Math.Floor(performance.Audience / 5m);
            return credits;
        }
    }
}