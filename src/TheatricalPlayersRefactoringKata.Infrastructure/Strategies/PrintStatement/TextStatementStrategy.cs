using System.Globalization;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Strategies.PrintStatement;

public class TextStatementStrategy : IPrintStatementStrategy
{
    public string Print(StatementEntity statement)
    {
        var cultureInfo = new CultureInfo("en-US");
        var result = $"Statement for {statement.Customer}\n";

        foreach (var item in statement.Items)
        {
            result += string.Format(
                cultureInfo,
                "  {0}: {1:C} ({2} seats)\n",
                item.Name, item.AmountOwed, item.Seats
            );
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", statement.AmountOwed);
        result += $"You earned {statement.EarnedCredits} credits\n";

        return result;
    }
}