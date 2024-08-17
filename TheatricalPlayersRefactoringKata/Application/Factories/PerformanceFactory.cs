using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Services;

namespace TheatricalPlayersRefactoringKata.Application.Factories
{
    public static class PerformanceFactory
    {
        public static IPerformanceCalculator CreateCalculator(string genre)
        {
            return genre switch
            {
                "tragedy" => new TragedyCalculator(),
                "comedy" => new ComedyCalculator(),
                "historical" => new HistoricalCalculator(),
                _ => throw new ArgumentException($"Genre '{genre}' not supported")
            };
        }
    }
}
