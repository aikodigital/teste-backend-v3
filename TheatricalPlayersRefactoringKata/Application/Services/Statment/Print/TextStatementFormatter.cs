using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statment.Print;

public class TextStatementFormatter : IStatementPrinter
{
    public static string Print(Invoice invoice)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";
        var cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = perf.Play;

            volumeCredits += perf.CalculateCredits();

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name,
                Convert.ToDecimal((float)perf.Amount / 100), perf.Audience);
            totalAmount += perf.Amount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal((float)totalAmount / 100));
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }
}
