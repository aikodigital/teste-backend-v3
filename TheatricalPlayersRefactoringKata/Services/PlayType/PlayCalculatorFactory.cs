using System;

namespace TheatricalPlayersRefactoringKata.Services.PlayType;

public static class PlayCalculatorFactory
{
    public static IPlayCalculator Create(string playType)
    {
        return playType switch
        {
            "tragedy" => new TragedyPlayCalculator(),
            "comedy" => new ComedyPlayCalculator(),
            "history" => new HistoricalPlayCalculator(),
            _ => throw new ArgumentException($"Unknown play type: {playType}")
        };
    }
}

