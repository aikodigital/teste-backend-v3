using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinterService
{
    public static string Print(FileExtension fileExtension, Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var statementData = new List<PerformanceResult>();

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var calculator = CalculatorServiceFactory.Create(play.Type);

            var thisAmount = calculator.CalculateAmount(performance, play);
            var credits = calculator.CalculateCredits(performance, play);

            volumeCredits += credits;
            totalAmount += thisAmount;

            statementData.Add(
                new PerformanceResult
                    {
                        PlayName = play.Name,
                        Amount = thisAmount,
                        Audience = performance.Audience,
                        Credits = credits
                });
        }

        return ExtractFormatterServiceFactory
            .Create(fileExtension)
            .Format(invoice.Customer, statementData, totalAmount, volumeCredits);
    }
}
