using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services.InvoicePrice;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    public string Print(Invoice invoice)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = perf.Play;
            var thisAmount = PerformancePrice.Price(perf.Play.Lines, perf.Audience, perf.Play.Type);

            // add volume credits
            volumeCredits += PerformancePrice.Credits(perf.Audience, perf.Play.Type == PlayType.Comedy);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(((double)thisAmount) / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(((double)totalAmount) / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
