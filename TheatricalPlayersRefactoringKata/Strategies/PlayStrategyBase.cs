using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public abstract class PlayStrategyBase : IPlayStrategy
    {
        // Method to adjust the number of lines
        protected int AdjustLines(Play play)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            return lines;
        }

        // Each strategy must implement its own calculation
        public abstract int Calculate(Performance performance, Play play);
    }
}
