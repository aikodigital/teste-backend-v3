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

        Performance p = new Performance("asg", 20);
        decimal d = 3227 / 10;

        /*result += String.Format("aq {0} aq2 {1}", 
            (new HistoricalCalculator()).calculateAmount(p, 3227 / 10),
            (new TragedyCalculator()).calculateAmount(p, 3227/10)
        );
        */
        foreach (var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            decimal lines = play.Lines;

            lines = Math.Max(1000,Math.Min(lines,4000)); // between 1000 and 4000 only

            decimal thisAmount = lines/10;

            var calcPlayAmount = PlayCalculatorFactory.createCalculator(play.Type); 
            thisAmount = calcPlayAmount.calculateAmount(perf, thisAmount);

            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type)
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);
            totalAmount += thisAmount;
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
