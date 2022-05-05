using System;
using System.Globalization;
using System.Text;

namespace TheatricalPlayersRefactoringKata.Printers;

public class TextStatementPrinter : IStatementPrinter
{
    public string Print(Statement statement, CultureInfo cultureInfo = null)
    {
        cultureInfo ??= new CultureInfo("en-US");

        var result = new StringBuilder(320);

        result.AppendLine(cultureInfo, $"Statement for {statement.Customer}");

        foreach (var (perf, entry) in statement)
        {
            var play = perf.Play;
            decimal thisAmount = entry.Amount;

            // print line for this order
            result.AppendLine(cultureInfo, $"   {play.Name}: {thisAmount.ToString("C")} ({perf.Audience} seats)");
        }
        result.AppendLine(cultureInfo, $"Amount owed is {statement.TotalAmount.ToString("C")}");

        result.AppendLine(cultureInfo, $"You earned {statement.TotalCredits} credits");

        return result.ToString();
    }
}