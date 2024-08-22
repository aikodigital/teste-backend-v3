using System.Globalization;
using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core;

public static class StatementPrinter
{
    public static string Print(Invoice invoice)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";
        var cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = perf.Play;

            volumeCredits += perf.CalculateCredits();

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name,
                                    Convert.ToDecimal((float)perf.Amount / 100), perf.Audience);
            totalAmount += perf.Amount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal((float)totalAmount / 100));
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }

    public static string XmlPrint(Invoice invoice)
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
            totalAmount   += perf.Amount;
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
        var doc = new XDocument();
        doc.Declaration = new XDeclaration("1.0", "utf-8", null);
        doc.Add(statement);


        using var memoryStream = new MemoryStream();

        using (var streamWriter = new StreamWriter(memoryStream, new UTF8Encoding(true)))
        {
            doc.Save(streamWriter);
        }

        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
}