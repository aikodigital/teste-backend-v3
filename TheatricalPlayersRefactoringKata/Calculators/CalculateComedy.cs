using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class CalculateComedy : ICalculateStrategy
    {
        public readonly int MinLine = 1000;
        public readonly int MaxLine = 4000;
        public readonly int BaseSpec = 20;
        public readonly decimal AdditionalValue = 100.00m;
        public readonly decimal AdditionalValueForSpec = 5.00m;
        public readonly decimal ValueForSpec = 3.00m;
        public readonly int BaseValue = 10;
        public  decimal CalculateAmount(Play play, Performance performance)
        {
            decimal amount = 0.00m;

            var lines = play.Lines;

            if (lines < MinLine)
                lines = MinLine;
            if (lines > MaxLine)
                lines = MaxLine;

            amount += (decimal)lines / BaseValue;

            if (performance.Audience > 20)
            {
                amount += AdditionalValue + ((performance.Audience - BaseSpec) * AdditionalValueForSpec);

            }

            amount += ValueForSpec * performance.Audience;

            return amount;
        }

     
        public int CalculateCredits(Play play, Performance performance)
        {
            var credits = Math.Max(performance.Audience - 30, 0);
            credits += (int)Math.Floor((decimal)performance.Audience / 5);

            return credits;
        }

    
    }
}