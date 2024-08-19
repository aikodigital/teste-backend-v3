using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Utils;

namespace TheatricalPlayersRefactoringKata.Application.Services.Formatters
{
    public class XmlStatementFormatter : IStatementFormatter
    {
        public string Format(Invoice invoice, Dictionary<Guid, Play> plays, Dictionary<Performance, int> performanceAmounts, int volumeCredits, decimal totalAmount)
        {
            List<XElement> items = new() { };

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                decimal baseAmount = performanceAmounts[perf];

                var element = new XElement("Item",
                    new XElement("AmountOwed", Convert.ToDecimal(baseAmount / 100)),
                    new XElement("EarnedCredits", perf.Credits),
                    new XElement("Seats", perf.Audience)
                );

                items.Add(element);
            }

            var xml = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("Statement",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                    new XElement("Customer", invoice.Customer),
                    new XElement("Items", items),
                    new XElement("AmountOwed", Convert.ToDecimal(totalAmount / 100)),
                    new XElement("EarnedCredits", volumeCredits)
                )
            );

            using var wr = new Utf8StringWriter();

            xml.Save(wr);
            return wr.ToString();
        }
    }
}
