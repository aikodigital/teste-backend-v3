using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services.Base;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter : MainService
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";
        var cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = CalculateAmount(play, perf);
            volumeCredits += CalculateVolumeCredits(play, perf);

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100m, perf.Audience);
            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100m);
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }
}
