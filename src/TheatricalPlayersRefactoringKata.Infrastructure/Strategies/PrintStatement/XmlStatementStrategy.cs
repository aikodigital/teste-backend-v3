using System.Globalization;
using System.Xml;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Strategies.PrintStatement;

public class XmlStatementStrategy : IPrintStatementStrategy
{
    public string Print(StatementEntity statement)
    {
        var cultureInfo = new CultureInfo("en-US");

        var xml = new XmlDocument();

        var root = xml.CreateElement("Statement");
        root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        root.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        xml.AppendChild(root);

        var customer = xml.CreateElement("Customer");
        customer.InnerText = statement.Customer;
        root.AppendChild(customer);

        var items = xml.CreateElement("Items");
        foreach (var statementItem in statement.Items)
        {
            var item = xml.CreateElement("Item");

            var amountOwed = xml.CreateElement("AmountOwed");
            amountOwed.InnerText = statementItem.AmountOwed.ToString(cultureInfo);
            item.AppendChild(amountOwed);

            var earnedCredits = xml.CreateElement("EarnedCredits");
            earnedCredits.InnerText = statementItem.EarnedCredits.ToString();
            item.AppendChild(earnedCredits);

            var seats = xml.CreateElement("Seats");
            seats.InnerText = statementItem.Seats.ToString();
            item.AppendChild(seats);

            items.AppendChild(item);
        }

        root.AppendChild(items);

        var totalAmountOwed = xml.CreateElement("AmountOwed");
        totalAmountOwed.InnerText = statement.AmountOwed.ToString(cultureInfo);
        root.AppendChild(totalAmountOwed);

        var totalEarnedCredits = xml.CreateElement("EarnedCredits");
        totalEarnedCredits.InnerText = statement.EarnedCredits.ToString();
        root.AppendChild(totalEarnedCredits);

        return SerializeXmlDocument(xml);
    }

    private string SerializeXmlDocument(XmlDocument xml)
    {
        using var stringWriter = new StringWriter();
        var xmlWriterSettings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "  ",
            NewLineChars = "\r\n",
            NewLineHandling = NewLineHandling.Replace
        };

        using (var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
        {
            xml.WriteTo(xmlWriter);
        }

        return stringWriter.ToString();
    }
}