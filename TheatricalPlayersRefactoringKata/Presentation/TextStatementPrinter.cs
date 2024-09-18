using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Presentation;

public class TextStatementPrinter : IStatementFormatter
{
    public string Print(StatementResult statement)
    {
        var result = $"Statement for {statement.Customer}\n";
        var cultureInfo = new CultureInfo("en-US");

        foreach (var line in statement.Lines)
        {
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", line.PlayName, line.Amount / 100, line.Audience);
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", statement.TotalAmount / 100);
        result += $"You earned {statement.TotalVolumeCredits} credits\n";

        return result;
    }
}
