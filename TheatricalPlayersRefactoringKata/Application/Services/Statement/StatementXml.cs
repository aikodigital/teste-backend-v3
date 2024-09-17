using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statement
{
    public class StatementXml : IStatement
    {
        public string Print(Invoice invoice)
        {
            var perfomances = invoice.Performances.Values;

            var items = new XElement("Items");
            foreach (var perf in perfomances)
            {
                items.Add(
                    new XElement("Item",
                        new XElement("AmountOwed", perf.CalculateTotalCost()),
                        new XElement("EarnedCredits", perf.CalculateCredits()),
                        new XElement("Seats", perf.Audience)
                    )
                );
            }

            var customerName = invoice.Customer.Name;

            XDocument xDocument = new(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("Statement",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                    new XElement("Customer", customerName),
                    items,
                    new XElement("AmountOwed", perfomances.Sum(perf => perf.CalculateTotalCost())),
                    new XElement("EarnedCredits", perfomances.Sum(perf => perf.CalculateCredits()))
                )
            );

            var xmlString = "";
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                xDocument.Save(writer);
                xmlString = Encoding.UTF8.GetString(stream.ToArray());
            }

            return xmlString.ToString();
        }
    }
}
