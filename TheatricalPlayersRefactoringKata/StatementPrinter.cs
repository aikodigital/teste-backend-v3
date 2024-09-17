using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public static string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;

        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        var cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;

            if (lines < 1000)
                lines = 1000;

            if (lines > 4000)
                lines = 4000;

            double thisAmount = lines / 10.0;

            thisAmount = play.Type switch
            {
                Play.TypePlay.Tragedy => AmountTragedy(perf, thisAmount),
                Play.TypePlay.Comedy => AmountComedy(perf, thisAmount),
                Play.TypePlay.History => AmountHistory(perf, thisAmount),
                _ => throw new Exception("unknown type: " + play.Type),
            };

            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);

            // add extra credit for every ten comedy attendees
            if (play.Type == Play.TypePlay.Comedy)
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);

            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += string.Format("You earned {0} credits\n", volumeCredits);

        return result;
    }

    private static double AmountTragedy(Performance perf, double thisAmount)
    {
        if (perf.Audience > 30)
        {
            thisAmount += 10.00 * (perf.Audience - 30);
        }

        return thisAmount;
    }

    private static double AmountComedy(Performance perf, double thisAmount)
    {
        if (perf.Audience > 20)
        {
            thisAmount += 100.00 + 5.00 * (perf.Audience - 20);
        }

        thisAmount += 3.00 * perf.Audience;
        
        return thisAmount;
    }

    private static double AmountHistory(Performance perf, double thisAmount)
    {
        return AmountComedy(perf, thisAmount) + AmountTragedy(perf, thisAmount);
    }
}
