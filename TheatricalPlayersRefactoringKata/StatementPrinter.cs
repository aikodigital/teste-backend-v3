using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            decimal thisAmount = lines * 10;
            switch (play.Type) 
            {
                case "tragedy":
                    thisAmount = CalculateTragedyAmount(perf, thisAmount);
                    break;
                case "comedy":
                    thisAmount = CalculateGenderComedy(perf, thisAmount);
                    break;
                case "history":
                    thisAmount = CalculateTragedyAmount(perf, thisAmount) + CalculateGenderComedy(perf, thisAmount);
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

    private static decimal CalculateTragedyAmount(Performance perf, decimal thisAmount)
    {
        var baseAmount = thisAmount;
        if (perf.Audience > 30)
        {
            baseAmount += 1000 * (perf.Audience - 30);
        }

        return baseAmount;
    }

    private static decimal CalculateGenderComedy(Performance perf, decimal thisAmount)
    {
        var baseAmount = thisAmount;
        baseAmount += (300 * perf.Audience);

        if (perf.Audience > 20)
        {
            baseAmount +=  10000 + (500 * (perf.Audience - 20));
        }

        return baseAmount;
    }
}
