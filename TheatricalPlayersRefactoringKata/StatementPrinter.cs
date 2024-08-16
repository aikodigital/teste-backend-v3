using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var baseAmount = lines / 10.0;
            var thisAmount = baseAmount;
            switch (play.Type)
            {
                case "tragedy":
                    if (perf.Audience > 30)
                    {
                        thisAmount += 10 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    thisAmount += 3 * perf.Audience;
                    if (perf.Audience > 20)
                    {
                        thisAmount += 100 + 5 * (perf.Audience - 20);
                    }
                    break;
                case "history":
                    var tragedyAmount = baseAmount;
                    if (perf.Audience > 30)
                    {
                        tragedyAmount += 10 * (perf.Audience - 30);
                    }

                    var comedyAmount = baseAmount + (3 * perf.Audience);
                    if (perf.Audience > 20)
                    {
                        comedyAmount += 100 + 5 * (perf.Audience - 20);
                    }

                    thisAmount = tragedyAmount + comedyAmount;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy/history attendees
            if (play.Type == "comedy")
            {
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            }
            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);
            totalAmount += (int)Math.Round(thisAmount * 100);
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100.0));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
