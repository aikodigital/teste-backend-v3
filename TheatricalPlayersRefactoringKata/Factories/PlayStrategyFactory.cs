
using System;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Strategies;

namespace TheatricalPlayersRefactoringKata.Factories
{
    public class PlayStrategyFactory
    {
        public static IPlayTypeStrategy GetStrategy(string type)
        {
            return type switch
            {
                "tragedy" => new TragedyStrategy(),
                "comedy"  => new ComedyStrategy(),
                "history" => new HistoryStrategy(),
                _ => throw new ArgumentException("Invalid play type")
            };
        }
    }
}
