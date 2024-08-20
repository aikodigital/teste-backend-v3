using System;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;

namespace TheatricalPlayersRefactoringKata.Services
{
    public static class BillProvider
    {
        public static IPlayCalculator GetCalculatorByType(string playType) => playType.ToLower() switch
        {
            "tragedy" => new TragedyCalculator(),
            "comedy" => new ComedyCalculator(),
            "history" => new HistoryCalculator(),
            _ => throw new Exception("unknown type: " + playType),
        };
    }
}