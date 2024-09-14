using System;
using TheatricalPlayersRefactoringKata.Calculator;
using TheatricalPlayersRefactoringKata.Interface;

namespace TheatricalPlayersRefactoringKata.Factory;
public static class PlayCalculatorFactory
{
    public static IPlayCalculator GetCalculator(string type) => type switch
    {
        "tragedy" => new TragedyCalculator(),
        "comedy" => new ComedyCalculator(),
        "history" => new HistoryCalculator(),
        _ => throw new InvalidOperationException($"Unknown play type: {type}")
    };
}

