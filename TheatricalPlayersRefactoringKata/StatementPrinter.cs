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

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            int thisAmount = CalculateAmountByPlayTypeAndLines(lines, perf.Audience, play.Type);

            // add volume credits
            volumeCredits += CalculateVolumeCreditsByPlayAudienceAndType(perf.Audience, play.Type);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    private int CalculateVolumeCreditsByPlayAudienceAndType(int audience, string playType)
    {
        int volumeCredits = Math.Max(audience - 30, 0);

        if(playType == "comedy") volumeCredits += (int)Math.Floor((decimal)audience / 5);

        return volumeCredits;
    }

    private int CalculateAmountByPlayTypeAndLines(int lines, int audience, string playType)
    {
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;

        int totalAmount = lines * 10;

        switch (playType)
        {
            case "tragedy":
                if (audience > 30)
                {
                    totalAmount += 1000 * (audience - 30);
                }
                break;
            case "comedy":
                if (audience > 20)
                {
                    totalAmount += 10000 + 500 * (audience - 20);
                }
                totalAmount += 300 * audience;
                break;
            default:
                throw new Exception("unknown type: " + playType);
        }
        return totalAmount;
    }
}
