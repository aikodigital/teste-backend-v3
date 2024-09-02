using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Strategies.Exports;

/// <summary>
/// Strategy for exporting invoice statements in XML format.
/// </summary>
public class XmlStatementExportStrategy : IStatementExportStrategy
{
    public string GenerateStatement(Invoice invoice)
    {
        decimal totalAmount = 0;
        var totalCredits = 0;

        // Create XML elements for each performance
        var itemsElement = new XElement("Items",
            from performance in invoice.Performances
            select new XElement("Item",
                new XElement("AmountOwed", $"{performance.Amount:F1}"),
                new XElement("EarnedCredits", performance.Credits),
                new XElement("Seats", performance.Audience)
            )
        );

        // Calculate total amount owed and credits earned
        foreach (var performance in invoice.Performances)
        {
            totalAmount += performance.Amount;
            totalCredits += performance.Credits;
        }

        // Create the main XML element
        var statementElement = new XElement("Statement",
            new XElement("Customer", invoice.Customer.Name),
            itemsElement,
            new XElement("AmountOwed", $"{totalAmount:F1}"),
            new XElement("EarnedCredits", totalCredits)
        );

        var xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), statementElement);
        return xmlDocument.ToString();
    }

    /// <summary>
    /// Exports the invoice statement to a TXT file.
    /// </summary>
    /// <param name="invoice">The invoice to be exported.</param>
    /// <param name="directory">The directory where the TXT file will be saved.</param>
    public async Task ExportAsync(Invoice invoice, string directory)
    {
        var xmlDocument = GenerateStatement(invoice);
        var xmlPath = Path.Combine(directory, $"{invoice.Id}.xml");
        await File.WriteAllTextAsync(xmlPath, xmlDocument);
    }
}
