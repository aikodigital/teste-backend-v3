using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TheatricalPlayersRefactoringKata;

//Impressora de extratos
public class StatementPrinter
{
    public string Print(Invoice invoice, List<Play> plays)
    {
        if (invoice.Performances.ToList().Any(p => !plays.Contains(p.Play)))
            throw new ArgumentException();

        var result = string.Format("Statement for {0}\n", invoice.Customer);
        var cultureInfo = new CultureInfo("en-US");

        invoice.Calculute();

        invoice.Performances.ToList().ForEach(p => 
        result += PrintLineThisOrder(invoice, result, cultureInfo, p, p.Play, p.Play.BaseValue));
            

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmount);
        result += String.Format("You earned {0} credits\n", invoice.VolumeCredits);
        return result;
    }

    private string PrintLineThisOrder(Invoice invoice, string result, CultureInfo cultureInfo,
                                      Performance performance, Play play, int performanceAmount)
    {
        invoice.PerformancesAmountCurtumer.Add(play.Name, performanceAmount);
        return String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, performanceAmount, performance.Audience);
    }



}
