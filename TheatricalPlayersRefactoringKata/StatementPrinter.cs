using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TheatricalPlayersRefactoringKata;

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
        result += String.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", p.PlayName, p.Amount, p.Audience));
        result += String.Format(_cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmount);
        result += String.Format("You earned {0} credits\n", invoice.VolumeCredits);
        
        return result;
    }
}
