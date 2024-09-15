using System;
using TheatricalPlayersRefactoringKata.Models;

public static class PlayCalculatorFactory
{
    public static IPlayCalculator CreateCalculator(Performance performance, Play play)
    {
        return play.Type switch
        {
            "tragedy" => new TragedyCalculator(performance, play),
            "comedy" => new ComedyCalculator(performance, play),
            "history" => new HistoryCalculator(performance, play),
            _ => throw new Exception($"Unknown play type: {play.Type}")
        };
    }
}
