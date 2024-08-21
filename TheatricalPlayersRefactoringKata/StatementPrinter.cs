using TheatricalPlayersRefactoringKata.Models;
using System.Collections.Generic;
using System.Globalization;
using System;

namespace TheatricalPlayersRefactoringKata.Printing;
public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        int totalVolumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            double thisAmount = play.Calculator.CalculateAmount(perf, play);

            int volumeCredits = play.Calculator.CalculateCredits(perf);
            totalVolumeCredits += volumeCredits;

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", totalVolumeCredits);
        return result;
    }
}