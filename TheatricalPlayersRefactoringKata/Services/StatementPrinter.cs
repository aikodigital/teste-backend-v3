using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, IPlay> plays)
    {
        decimal totalAmount = 0;
        int volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = play.CalculateAmount(perf.Audience);
            volumeCredits += play.CalculateVolumeCredits(perf.Audience);

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100, perf.Audience);
            totalAmount += thisAmount;
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100);
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
