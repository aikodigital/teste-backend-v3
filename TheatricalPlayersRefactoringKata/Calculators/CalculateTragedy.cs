using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class CalculateTragedy : ICalculateStrategy
    {

        public readonly int MinLine = 1000;
        public readonly int MaxLine = 4000;
        public readonly int BaseSpec = 30;
        public readonly decimal AdditionalValueForSpec = 10.00m;
        public readonly int BaseValue = 10;
        public decimal CalculateAmount(Play play, Performance performance)
        {
            decimal amount = 0;

            var lines = play.Lines;

            if (lines < MinLine)
                lines = MinLine;
            if (lines > MaxLine)
                lines = MaxLine;

            amount += (decimal)lines / BaseValue;

            if(performance.Audience > BaseSpec)
            {
                amount += AdditionalValueForSpec * (performance.Audience - BaseSpec);
            }

            return amount;
        }

        public int CalculateCredits(Play play, Performance performance)
        {
            var credits = Math.Max(performance.Audience - 30, 0);
            return credits;
        }
    }
}
