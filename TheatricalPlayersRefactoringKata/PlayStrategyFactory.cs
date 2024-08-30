using System;

namespace TheatricalPlayersRefactoringKata
{
    public class PlayStrategyFactory
    {
        public static IPlayStrategy CreateStrategy(string type)
        {
            switch (type)
            {
                case "tragedy":
                    return new TragedyStrategy();
                case "comedy":
                    return new ComedyStrategy();
                case "history":
                    return new HistoryStrategy();
                default:
                    throw new Exception("unknown type: " + type);
            }
        }
    }
}
