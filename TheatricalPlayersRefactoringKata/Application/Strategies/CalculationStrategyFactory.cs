using System;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Strategies;

public class CalculationStrategyFactory
{
    public ICalculationStrategy GetStrategy(string playType)
    {
        return playType switch
        {
            "tragedy" => new TragedyCalculationStrategy(),
            "comedy" => new ComedyCalculationStrategy(),
            "history" => new HistoryCalculationStrategy(this),
            _ => throw new Exception("Unknown play type: " + playType)
        };
    }
}
