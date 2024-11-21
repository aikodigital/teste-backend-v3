using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Formatters
{
    public class XmlStatementFormatter : IStatementFormatter
    {
        public string Format(Invoice invoice)
        {
            var statement = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items",
                    invoice.Performances.Select(performance => new XElement("Item",
                        new XElement("AmountOwed", performance.FormattedCost),
                        new XElement("EarnedCredits", performance.Credits),
                        new XElement("Seats", performance.Audience)
                    ))
                ),
                new XElement("AmountOwed", invoice.FormattedTotalCost),
                new XElement("EarnedCredits", invoice.TotalCredits)
            );

            var declaration = new XDeclaration("1.0", "utf-8", null);
            var xmlDocument = new XDocument(declaration, statement);

            using (var memoryStream = new MemoryStream())
            {
                var settings = new XmlWriterSettings
                {
                    Encoding = new UTF8Encoding(true),
                    Indent = true
                };

                using (var writer = XmlWriter.Create(memoryStream, settings))
                {
                    xmlDocument.Save(writer);
                }

                var Xml = Encoding.UTF8.GetString(memoryStream.ToArray());

                // Have to do it because the BOM (Byte Order Mark) in txt approve file encoding...
                return Xml;
            }

        }

    }
}
