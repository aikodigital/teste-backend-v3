using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

//Impressora de extratos
public class StatementPrinter
{
    public string Print(Invoice invoice, List<Play> plays)
    {
        var volumeCredits = 0;

        var result = string.Format("Statement for {0}\n", invoice.Customer);
        var cultureInfo = new CultureInfo("en-US");

        foreach(var performance in invoice.Performances)
        {
            if (!plays.Contains(performance.Play)) throw new ArgumentException();
            
            var baseValue = performance.Play.CalculateBaseValue(performance);

            result = PrintLineThisOrder(invoice, result, cultureInfo, performance, performance.Play, ref baseValue);

        }


        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmount);
        result += String.Format("You earned {0} credits\n", invoice.VolumeCredits);
        return result;
    }

    private string PrintLineThisOrder(Invoice invoice, string result, CultureInfo cultureInfo,
                                      Performance performance, Play play, ref int performanceAmount)
    {
        invoice.PerformancesAmountCurtumer.Add(play.Name, performanceAmount);
        result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, performanceAmount, performance.Audience);
        return result;
    }



}
