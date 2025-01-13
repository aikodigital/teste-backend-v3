using TheatricalPlayersRefactoringKata.Application.Services.CalculatorGenre;
using TheatricalPlayersRefactoringKata.Application.Services.Mapping;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.StatementManipulate
{
    public static class StatementDataService
    {
        public static List<Statement> StatementsData(Invoice invoice, Dictionary<string, Play> plays)
        {
            var statements = new List<Statement>();

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];

                if (MappingService.GenreMapping.TryGetValue(play.Type, out var Type))
                {
                    // creates an object of class of type of item actual of foreach
                    var instanceCalculatorGenre = Activator.CreateInstance(Type) as ICalculatorGenre;

                    // creates an object of class of context that redirect to a class specific to make of the calcule of the values
                    var calculatorGenreContext = new CalculatorGenreContext(instanceCalculatorGenre!, play.Lines, perf.Audience);

                    // calculates of values of performance
                    var performanceValues = calculatorGenreContext.CalculatePerformanceOfValues();

                    statements.Add(new Statement(play.Name, performanceValues.amount, perf.Audience, performanceValues.credit));
                }
                else
                {
                    throw new Exception("Unknown type: " + play.Type);
                }
            }

            return statements;
        }
    }
}
