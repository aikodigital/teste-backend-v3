using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TheatricalPlayersRefactoringKata;

//Impressora de extratos
public class StatementPrinter
{
    private readonly CultureInfo _cultureInfo;
    
    public StatementPrinter()
    {
        _cultureInfo = new CultureInfo("en-US");
    }


    public string Print(Invoice invoice)
    {
        var result = string.Format("Statement for {0}\n", invoice.Customer);

        invoice.Performances.ToList().ForEach(p => 
        result += PrintLineThisOrder(invoice, result, p, p.Play, p.Play.BaseValue));
        result += String.Format(_cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmount);
        result += String.Format("You earned {0} credits\n", invoice.VolumeCredits);
        
        return result;
    }

    private string PrintLineThisOrder(Invoice invoice, string result,
                                      Performance performance, Play play, int performanceAmount)
    {
        return String.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, performanceAmount, performance.Audience);
    }



}
