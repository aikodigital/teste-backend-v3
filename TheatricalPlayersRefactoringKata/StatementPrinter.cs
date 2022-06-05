using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0m;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10m;
            switch (play.Type) 
            {
                case "tragedy":
                    thisAmount = CalculateTragedyValue(perf.Audience, thisAmount);
                    
                    break;
                case "comedy":
                    thisAmount = CalculateComedyValue(perf.Audience, thisAmount);
                    break;
                case "history":
                    var tragedyAmount = CalculateTragedyValue(perf.Audience, thisAmount);
                    var comedyAmount = CalculateComedyValue(perf.Audience, thisAmount);

                    thisAmount = (tragedyAmount + comedyAmount);

                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
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

    private decimal CalculateTragedyValue(int audience, decimal amount)
    {
        if (audience > 30)
            amount += 1000 * (audience - 30);

        return amount;
    }

    private decimal CalculateComedyValue(int audience, decimal amount)
    {
        if (audience > 30)
            amount += 10000 + 500 * (audience - 20);

        amount += 300 * audience;

        return amount;
    }
}
