using System;
using TheatricalPlayersRefactoringKata.Calculators;

namespace TheatricalPlayersRefactoringKata
{
    public class CalculatorFactory : ICalculatorFactory
    {
        public ICalculator GetCalculator(string type)
        {
            return type switch
            {
                "tragedy" => new TragedyCalculator(),
                "comedy" => new ComedyCalculator(),
                "history" => new HistoryCalculator(),
                _ => throw new ArgumentException("Unknown type", nameof(type)),
            };
        }
    }
}
