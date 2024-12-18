using System.Globalization;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services.Formatters;

public class TextStatementFormatter : IStatementFormatter
{
    public string Format(Statement statement)
    {
        var cultureInfo = new CultureInfo("en-US");
        var result = $"Statement for {statement.Customer}\n";

        foreach (var item in statement.Items)
        {
            result += $"  {item.PlayName}: {item.AmountOwed.ToString("C", cultureInfo)} ({item.Seats} seats)\n";
        }

        result += $"Amount owed is {statement.TotalAmount.ToString("C", cultureInfo)}\n";
        result += $"You earned {statement.TotalCredits} credits\n";

        return result;
    }
}
