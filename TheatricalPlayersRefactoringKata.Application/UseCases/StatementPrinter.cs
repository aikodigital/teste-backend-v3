using System.Xml.Serialization;
using System.Xml;
using TheatricalPlayersRefactoringKata.Application.Utils;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    public string Print(Invoice invoice)
    {
        return invoice.PrintInvoiceStatement();
    }

    public string PrintXmlStatement(Invoice invoice)
    {
        var items = new List<StatementItem>();

        foreach (var performance in invoice.Performances)
        {
            decimal amountOwed = performance.GetPlayAmountValue();
            int earnedCredits = performance.GetPlayCreditsValue();
            int seats = performance.Audience;
            var item = StatementItem.Create(amountOwed, earnedCredits, seats);
            items.Add(item);
        }

        var invoiceStatement = Statement.Create(invoice.Customer, items);

        var xmlSerializer = new XmlSerializer(typeof(Statement));

        using (var stringWriter = new Utf8StringWriter())
        {
            using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
            {
                xmlSerializer.Serialize(writer, invoiceStatement);
                return stringWriter.ToString();
            }
        }
    }
}
