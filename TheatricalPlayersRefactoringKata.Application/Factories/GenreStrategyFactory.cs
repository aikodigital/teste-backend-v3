using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Strategies;

namespace TheatricalPlayersRefactoringKata.Application.Factories
{
    public static class GenreStrategyFactory
    {
        public static IGenreStrategy Create(string genre) => genre switch
        {
            "tragedy" => new TragedyStrategy(),
            "comedy" => new ComedyStrategy(),
            "history" => new HistoryStrategy(),
            _ => throw new ArgumentException($"Unknown genre: {genre}")
        };
    }
}
