using System;
using TheatricalPlayersRefactoringKata.Calculators;

namespace TheatricalPlayersRefactoringKata.Core
{
    public static class CalculatorFactory
    {
        public static ICalculator CreateCalculator(Play play)
        {
            return play.Type switch
            {
                "tragedy" => new TragedyCalculator(),
                "comedy" => new ComedyCalculator(),
                "history" => new HistoryCalculator(),
                _ => throw new Exception("unknown type: " + play.Type),
            };
        }
    }
}
