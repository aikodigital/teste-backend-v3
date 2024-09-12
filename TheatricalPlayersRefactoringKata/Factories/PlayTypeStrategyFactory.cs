using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Strategies;

namespace TheatricalPlayersRefactoringKata.Factories
{
    public static class PlayTypeStrategyFactory
    {
        private static readonly Dictionary<PlayType, IPlayTypeStrategy> _strategies = new Dictionary<PlayType, IPlayTypeStrategy>
    {
        { PlayType.Tragedy, new TragedyStrategy() },
        { PlayType.Comedy, new ComedyStrategy() },
        { PlayType.History, new HistoricalStrategy() }
    };

        public static IPlayTypeStrategy GetStrategy(PlayType playType)
        {
            if (_strategies.TryGetValue(playType, out var strategy))
            {
                return strategy;
            }

            throw new ArgumentException("Unknown play type: " + playType);
        }
    }
}
