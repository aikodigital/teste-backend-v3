using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice)
    {
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        decimal totalAmount = invoice.CalculateTotals();
        var volumeCredits = invoice.CalculateCredits();

        foreach (var perf in invoice.Performances) 
        {

            var thisAmount = perf.CalculateValue();

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", perf.play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
