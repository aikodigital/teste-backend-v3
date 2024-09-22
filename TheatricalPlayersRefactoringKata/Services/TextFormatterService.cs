using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class TextFormatterService : IFormatter
{
    public string Format(string customer, List<PerformanceResult> performances, int totalAmount, int credits)
    {
        var cultureInfo = new CultureInfo("en-US");
        var result = $"Statement for {customer}\n";
        foreach (var performance in performances)
        {
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", performance.PlayName, performance.Amount / 100m, performance.Audience);
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100m);
        result += $"You earned {credits} credits\n";
        return result;
        ;
    }
}
