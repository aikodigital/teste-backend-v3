using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Factories;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");
        

        foreach (var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            var strategy = PlayTypeStrategyFactory.GetStrategy(play.Type);
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = strategy.CalculateAmount(lines, perf.Audience);
            volumeCredits += strategy.CalculateVolumeCredits(perf.Audience);
           

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
