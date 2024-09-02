using System.Text;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Strategies.Exports;

/// <summary>
/// Strategy for exporting invoice statements in TXT format.
/// </summary>
public class TxtStatementExportStrategy : IStatementExportStrategy
{
    public string GenerateStatement(Invoice invoice)
    {
        decimal totalAmount = 0;
        var totalCredits = 0;

        var sb = new StringBuilder();
        sb.AppendLine($"Statement for {invoice.Customer.Name}");

        // Add information for each performance
        foreach (var performance in invoice.Performances)
        {
            // Use standard currency formatting and ensure consistent layout
            sb.AppendLine($" {performance.Play.Title}: {performance.Amount:C} ({performance.Audience} seats)");

            totalAmount += performance.Amount;
            totalCredits += performance.Credits;
        }

        // Add total amount owed and credits earned
        sb.AppendLine($"Amount owed is {totalAmount:C}");
        sb.AppendLine($"You earned {totalCredits} credits");

        return sb.ToString();
    }

    /// <summary>
    /// Exports the invoice statement to a TXT file.
    /// </summary>
    /// <param name="invoice">The invoice to be exported.</param>
    /// <param name="directory">The directory where the TXT file will be saved.</param>
    public async Task ExportAsync(Invoice invoice, string directory)
    {
        var txtDocument = GenerateStatement(invoice);
        var txtPath = Path.Combine(directory, $"{invoice.Id}.txt");
        await File.WriteAllTextAsync(txtPath, txtDocument);
    }
}
