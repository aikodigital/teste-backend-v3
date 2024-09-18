using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Strategies;

namespace TheatricalPlayersRefactoringKata.Calculatros
{
    public class PlayAmountCalculator
    {
        private readonly Dictionary<string, IPlayStrategy> _strategies;

        public PlayAmountCalculator()
        {
            _strategies = new Dictionary<string, IPlayStrategy>
        {
            { "tragedy", new TragedyStrategy() },
            { "comedy", new ComedyStrategy() },
            { "history", new HistoryStrategy() }
        };
        }

        public int Calculate(Performance performance, Play play)
        {
            if (!_strategies.ContainsKey(play.Type))
            {
                throw new Exception("Unknown play type: " + play.Type);
            }

            var strategy = _strategies[play.Type];
            return strategy.Calculate(performance, play);
        }
    }
}
