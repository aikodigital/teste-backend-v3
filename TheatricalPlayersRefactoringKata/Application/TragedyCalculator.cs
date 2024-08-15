using System;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application
{
    public class TragedyCalculator : IPlayTypeCalculator
    {
        public bool CanHandle(string playType)
        {
            return playType == "tragédia";
        }

        public decimal CalculateAmount(Play play, Performance performance)
        {
            var baseAmount = play.Price / 10;
            if (performance.Audience > 30)
            {
                baseAmount += 10 * (performance.Audience - 30);
            }
            return baseAmount;
        }

        public int CalculateCredits(Play play, Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}