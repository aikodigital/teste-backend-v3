using TheatricalPlayersRefactoringKata.Application.UseCases.Calculators;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Factories
{
    public class TheatricalCalculatorFactory
    {
        public static ITheatricalCalculator GetCalculator(string genreType)
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
