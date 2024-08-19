using System;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class BillProvider
    {
        public IPlayCalculator GetCalculatorByType(string playType) => playType switch
        {
            "tragedy" => new TragedyCalculator(),
            "comedy" => new ComedyCalculator(),
            "history" => new HistoryCalculator(),
            _ => throw new Exception("unknown type: " + playType),
        };
    }
}