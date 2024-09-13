using System;

namespace TheatricalPlayersRefactoringKata
{
    public class PlayAmountCalculatorFactory
    {
        public static IPlayAmountCalculator GetCalculator(string playType)
        {
            return playType switch
            {
                "tragedy" => new TragedyAmountCalculator(),
                "comedy" => new ComedyAmountCalculator(),
                "history" => new HistoryAmountCalculator(),
                _ => throw new Exception("unknown type: " + playType)
            };
        }
    }
}
