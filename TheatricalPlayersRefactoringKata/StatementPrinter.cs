using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{

    private static int CalculateAmount(string type, int baseValue, int audience)
    {
        switch (type)
        {
            case "tragedy":
                return baseValue + 1000 * Math.Max(audience - 30, 0);
            case "comedy":
                return baseValue + 500 * Math.Max(audience - 20, 0) + 300 * audience + 10000 * (int)Math.Min(Math.Floor((double)(audience / 21)), 1);
            case "history":
                return 2 * baseValue + 1000 * Math.Max(audience - 30, 0) + 500 * Math.Max(audience - 20, 0) + 300 * audience + 10000 * (int)Math.Min(Math.Floor((double)(audience / 21)), 1);
            default:
                return 0;
        }
    }

    private static int CalculateCredits(string type, int baseValue, int audience)
    {
        switch (type)
        {
            case "comedy":
                return baseValue + (int)Math.Floor((decimal)audience / 5);
            default:
                return baseValue;
        }
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            Console.WriteLine(string.Format("Performance of: {0}\n", play.Name));
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            Console.WriteLine(string.Format("Lines: {0}\n", lines));
            Console.WriteLine(string.Format("audience: {0}\n", perf.Audience));
            var thisAmount = lines * 10;
            Console.WriteLine(string.Format("thisAmount: {0}\n", thisAmount));
            // calculate type credits and amount
            thisAmount = CalculateAmount(play.Type, thisAmount, perf.Audience);
            volumeCredits = CalculateCredits(play.Type, volumeCredits, perf.Audience);
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount) / 100, perf.Audience);
            totalAmount += thisAmount;
        }
        Console.WriteLine(String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount) / 100));
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount) / 100);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
