using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services.Calculators;

namespace TheatricalPlayersRefactoringKata.Application.Services.Factories
{
    public class GenreCalculatorFactory
    {
        public static IGenreCalculator GetCalculator(string genreType)
        {
            return genreType switch
            {
                "tragedy" => new TragedyCalculator(),
                "comedy" => new ComedyCalculator(),
                "history" => new HistoryCalculator(),
                _ => throw new Exception("unknown genre: " + genreType),
            };
        }
    }
}
