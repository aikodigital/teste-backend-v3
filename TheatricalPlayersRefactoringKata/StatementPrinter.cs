using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;
public class StatementPrinter
{
    public string Print(Invoice invoice)
    {
        var result = $"Statement for {invoice.Customer}\n";
        var cultureInfo = new CultureInfo("en-US");

        foreach (var performance in invoice.Performances)
        {
            if (performance.Play == null)
            {
                throw new InvalidOperationException("A peça associada à performance não foi encontrada.");
            }
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n",
                performance.Play.Name,
                performance.CalculateAmount() / 100,
                performance.Audience);
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmount() / 100);
        result += $"You earned {invoice.TotalCredits()} credits\n";
        return result;
    }
}
