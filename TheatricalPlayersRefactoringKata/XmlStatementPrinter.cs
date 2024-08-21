using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace TheatricalPlayersRefactoringKata
{
    public interface IStatementPrinter
    {
        string Print(Invoice invoice);
    }

    public class XmlStatementPrinter : IStatementPrinter
    {
        public string Print(Invoice invoice)
        {
            var statement = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("Statement",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                    new XElement("Customer", invoice.Customer),
                    new XElement("Items",
                        from performance in invoice.Performances
                        select new XElement("Item",
                            new XElement("AmountOwed", performance.CalculateAmount() / 100),
                            new XElement("EarnedCredits", performance.CalculateCredits()),
                            new XElement("Seats", performance.Audience))),
                    new XElement("AmountOwed", invoice.TotalAmount() / 100),
                    new XElement("EarnedCredits", invoice.TotalCredits())
                )
            );

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream, new UTF8Encoding(true)))
                {
                    statement.Save(writer);
                }
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}