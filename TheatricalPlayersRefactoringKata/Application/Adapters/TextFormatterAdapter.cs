using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Adapters;

public class TextFormatterAdapter : IFormatterAdapter
{
    public string Format(Statement statement)
    {
        var cultureInfo = new CultureInfo("en-US");
        var result = $"Statement for {statement.Customer}\n";

        foreach (var item in statement.Items)
        {
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n",
                item.Name, item.AmountOwed, item.Seats);
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", statement.TotalAmountOwed);
        result += $"You earned {statement.TotalEarnedCredits} credits\n";

        return result;
    }
}

