using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var amountTragedy = 0;
        var amountComedy = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId.ToString()];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines / 10;
            switch (play.Type) 
            {
                case "tragedy":
                    if (perf.Audience >= 30) {
                        amountTragedy = thisAmount += 10 * perf.Audience;
                    }
                    break;
                case "comedy":
                    if (perf.Audience > 20) {
                        thisAmount += 100 + 5 * perf.Audience;
                    }
                     amountComedy = thisAmount += 3 * perf.Audience;
                    break;
                case "history":
                    var amountHistory = amountTragedy + amountComedy;
                    break;
                default:
                    throw new System.Exception("unknown type: " + play.Type);
            }
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
            if (perf.Audience > 30)
            {
                perf.Audience += 1;
            }
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
