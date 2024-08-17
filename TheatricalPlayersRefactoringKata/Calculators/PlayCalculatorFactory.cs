using System;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal class PlayCalculatorFactory
    {
        public IPlayCalculator GetCalculator(string playType)
        {
            return playType switch
            {
                "tragedy" => new TragedyCalculator(),
                "comedy" => new ComedyCalculator(),
                "history" => new HistoryCalculator(),
                _ => throw new Exception("unknown type: " + playType),
            };
        }
    }
}
