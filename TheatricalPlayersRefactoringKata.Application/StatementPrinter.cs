using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{

    public string Print(Invoice invoice, Dictionary<string, Play> plays, string format)
    {
        foreach (var performance in invoice.Performances)
        {
            performance.Play = plays[performance.PlayId];
            var strategy = GenreStrategyFactory.Create(performance.Play.Type);

            performance.Cost = strategy.CalculateCost(performance.Audience, performance.Play.Lines);
            performance.Credits = strategy.CalculateCredits(performance.Audience);
        }

        var formatter = StatementFormatterFactory.Create(format);

        return formatter.Format(invoice);
    }
}
