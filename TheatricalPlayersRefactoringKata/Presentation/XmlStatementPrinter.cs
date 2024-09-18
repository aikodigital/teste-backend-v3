using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Helpers;

namespace TheatricalPlayersRefactoringKata.Presentation;

public class XmlStatementPrinter
{
    public string PrintXml(StatementResult statement)
    {
        XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

        XDocument doc = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                new XElement("Customer", statement.Customer),
                new XElement("Items")
            )
        );

        XElement itemsElement = doc.Root.Element("Items");

        foreach (var line in statement.Lines)
        {
            itemsElement.Add(
                new XElement("Item",
                    new XElement("AmountOwed", line.Amount / 100),
                    new XElement("EarnedCredits", line.VolumeCredits),
                    new XElement("Seats", line.Audience)
                )
            );
        }

        doc.Root.Add(
            new XElement("AmountOwed", statement.TotalAmount / 100),
            new XElement("EarnedCredits", statement.TotalVolumeCredits)
        );

        using (var writer = new Utf8StringWriter())
        {
            doc.Save(writer);
            return writer.ToString();
        }
    }
}
