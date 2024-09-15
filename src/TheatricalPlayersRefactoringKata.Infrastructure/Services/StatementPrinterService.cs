using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Enum;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services;

public class StatementPrinterService : IStatementPrinterService
{
    public string Print(InvoiceEntity invoice, Dictionary<string, PlayEntity> plays)
    {
        var cultureInfo = new CultureInfo("en-US");

        var totalAmount = 0m;
        var volumeCredits = 0;

        var result = $"Statement for {invoice.Customer}\n";

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var lines = play.Lines switch
            {
                < 1000 => 1000,
                > 4000 => 4000,
                _ => play.Lines
            };

            // calculate performance amount
            var performanceBaselineAmountUnit = lines / 10m;
            var performanceAmount = performanceBaselineAmountUnit * 100;
            switch (play.Type)
            {
                case PlayTypeEnum.Tragedy:
                    if (performance.Audience > 30)
                    {
                        performanceAmount += 1000 * (performance.Audience - 30);
                    }

                    break;

                case PlayTypeEnum.Comedy:
                    performanceAmount += 300 * performance.Audience;

                    if (performance.Audience > 20)
                    {
                        performanceAmount += 10000 + 500 * (performance.Audience - 20);
                    }

                    break;

                case PlayTypeEnum.History:
                    var tragedyAmount = performanceAmount;
                    if (performance.Audience > 30)
                    {
                        tragedyAmount += 1000 * (performance.Audience - 30);
                    }

                    var comedyAmount = performanceAmount + 300 * performance.Audience;
                    if (performance.Audience > 20)
                    {
                        comedyAmount += 10000 + 500 * (performance.Audience - 20);
                    }

                    performanceAmount = tragedyAmount + comedyAmount;
                    
                    break;

                default:
                    throw new Exception("unknown type: " + play.Type);
            }

            // add volume credits
            volumeCredits += Math.Max(performance.Audience - 30, 0);

            // add extra credit for every five comedy attendees
            if (play.Type == PlayTypeEnum.Comedy)
            {
                volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);
            }

            // print line for this order
            result += string.Format(
                cultureInfo,
                "  {0}: {1:C} ({2} seats)\n",
                play.Name, performanceAmount / 100, performance.Audience
            );
            totalAmount += performanceAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += $"You earned {volumeCredits} credits\n";

        return result;
    }
}