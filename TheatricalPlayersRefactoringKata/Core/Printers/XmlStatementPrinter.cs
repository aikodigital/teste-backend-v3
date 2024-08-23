#region

using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

#endregion

namespace TheatricalPlayersRefactoringKata.Core.Printers;

public abstract class XmlStatementPrinter : IStatementPrinter
{
    public static string Print(Invoice invoice)
    {
        var doc = XmlMount(invoice).Result;

        using var memoryStream = new MemoryStream();

        using (var streamWriter = new StreamWriter(memoryStream, new UTF8Encoding(true)))
        {
            doc.Save(streamWriter);
        }

        XmlSave(doc);

        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }

    public static Task<XDocument> XmlMount(Invoice invoice)
    {
        var totalAmount = 0;
        var volumeCredits = 0;

        var items = new XElement("Items");

        foreach (var perf in invoice.Performances)
        {
            var item = new XElement("Item");
            item.Add(new XElement("AmountOwed", Convert.ToDecimal((float)perf.Amount / 100)));
            item.Add(new XElement("EarnedCredits", perf.CalculateCredits()));
            item.Add(new XElement("Seats", perf.Audience));
            volumeCredits += perf.CalculateCredits();
            totalAmount += perf.Amount;
            items.Add(item);
        }

        var costumer = new XElement("Customer", invoice.Customer);
        var amountowed = new XElement("AmountOwed", Convert.ToDecimal((float)totalAmount / 100));
        var earnedcredits = new XElement("EarnedCredits", volumeCredits);
        var statement = new XElement("Statement",
            new XAttribute(XNamespace.Xmlns + "xsi",
                "http://www.w3.org/2001/XMLSchema-instance"),
            new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"));
        statement.Add(costumer);
        statement.Add(items);
        statement.Add(amountowed);
        statement.Add(earnedcredits);


        //<?xml version="1.0" encoding="utf-8"?>
        var doc = new XDocument
        {
            Declaration = new XDeclaration("1.0", "utf-8", null)
        };
        doc.Add(statement);

        return Task.FromResult(doc);
    }

    private static void XmlSave(XDocument doc)
    {
        var root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        var path = Path.Combine(root, "Archives");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        var fileName = Path.Combine(path, "statement.xml");
        doc.Save(fileName);
    }
}