using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        new XElement("AmountOwed", performance.Cost),
                        new XElement("EarnedCredits", performance.Credits),
                        new XElement("Seats", performance.Audience)
                    ))
                ),
                new XElement("AmountOwed", invoice.TotalCosts),
                new XElement("EarnedCredits", invoice.TotalCredits)
            );

            return statement.ToString();
        }
    }
}
