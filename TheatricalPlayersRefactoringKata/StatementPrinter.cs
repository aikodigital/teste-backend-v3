using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice)
    {
        decimal totalAmount = 0m;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = perf.Play;
            decimal lines = play.Lines;

            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            decimal baseAmount = lines / 10;

            decimal thisAmount = baseAmount;

            thisAmount += perf.CalculateAmmount();

            volumeCredits += perf.CalculateCredits();

            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
