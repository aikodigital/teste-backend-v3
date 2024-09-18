using System;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.Services.PlayTypeCalculators;

public static class PlayTypeCalculatorFactory
{
    public static IPlayTypeCalculator GetCalculator(string playType)
    {
        return playType switch
        {
            "tragedy" => new TragedyCalculator(),
            "comedy" => new ComedyCalculator(),
            "history" => new HistoryCalculator(),
            _ => throw new Exception($"Unknown play type: {playType}")
        };
    }
}
