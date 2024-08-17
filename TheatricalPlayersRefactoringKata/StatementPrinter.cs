using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Enums;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        int totalAmount = 0;
        int volumeCredits = 0;
        string result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            Play play = plays[perf.PlayId];
            int lines = play.Lines;
            Genre genre = play.Type switch
            {
                TheatricalType.Tragedy => new Tragedy(),
                TheatricalType.Comedy => new Comedy(),
                TheatricalType.History => new History(),
                _ => throw new Exception($"Unknown theatrical genre {play.Type}")
            };
            // calculate this amount
            int thisAmount = genre.CalculateAmount(perf.Audience, lines);
            // calculate volume credits
            volumeCredits += genre.CalculateVolumeCredits(perf.Audience);
            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100m), perf.Audience);
            totalAmount += thisAmount;
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100m));
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
