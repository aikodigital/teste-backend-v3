using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace TheatricalPlayersRefactoringKata.Modules;

public class TextOutputWritter : AbstractOutputWritter
{
    override public string FileType { get => "txt"; }

    override public byte[] GenerateOutput(Invoice invoice, CultureInfo cultureInfo)
    {
        if (invoice.Results == null)
        {
            throw new Exception("Results not calculated");
        }

        StringBuilder result = new StringBuilder();
        result.Append($"Statement for {invoice.Customer}\n");

        foreach (Performance performance in invoice.Performances)
        {
            if (performance.Results == null)
            {
                throw new Exception("Results not calculated for performance");
            }

            if (performance.Results.Play == null)
            {
                throw new Exception("Play not found for performance");
            }

            result.Append($"  {performance.Results.Play.Name}: {performance.Results.AmountOwed.ToString("C", cultureInfo)} ({performance.Audience} seats)\n");
        }

        result.Append($"Amount owed is {invoice.Results.TotalAmountOwed.ToString("C", cultureInfo)}\n");
        result.Append($"You earned {invoice.Results.TotalEarnedCredits} credits");

        return Encoding.UTF8.GetBytes(result.ToString());
    }
}