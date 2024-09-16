using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Services;

public class XmlStatementFormatter : IStatementFormatter
{
    public string Print(Invoice invoice)
    {
        var xDocument = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items",
                    from perf in invoice.Performances
                    select new XElement("Item",
                        new XElement("AmountOwed", Convert.ToDecimal(perf.CalculateValue() / 100)),
                        new XElement("EarnedCredits", perf.CalculateCredits()),
                        new XElement("Seats", perf.Audience))),
                new XElement("AmountOwed", Convert.ToDecimal(invoice.CalculateTotals() / 100)),
                new XElement("EarnedCredits", invoice.CalculateCredits())
            )
        );
        var memory = new MemoryStream();
        xDocument.Save(memory);
        return Encoding.UTF8.GetString(memory.ToArray());
    }
}