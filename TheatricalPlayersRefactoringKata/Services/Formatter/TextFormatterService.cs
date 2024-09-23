using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class TextFormatterService : IFormatter
{
    public string Format(string customer, List<PerformanceResult> performancesResult, int totalAmount, int credits)
    {
        var cultureInfo = new CultureInfo("en-US");
        var result = $"Statement for {customer}\n";

        foreach (var performanceResult in performancesResult)
        {
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", performanceResult.PlayName, performanceResult.Amount / 100m, performanceResult.Audience);
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100m);
        result += $"You earned {credits} credits\n";

        return result;
    }
}
