using System;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Services;

public class TextStatementFormatter : IStatementFormatter
{
    public string Print(Invoice invoice)
    {
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        try
        {
            cultureInfo = new CultureInfo("en-US");
        }
        catch (CultureNotFoundException)
        {
            cultureInfo = CultureInfo.InvariantCulture;
        }


        decimal totalAmount = invoice.CalculateTotals();
        var volumeCredits = invoice.CalculateCredits();

        foreach (var perf in invoice.Performances)
        {
            var thisAmount = perf.CalculateValue();
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", perf.play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}