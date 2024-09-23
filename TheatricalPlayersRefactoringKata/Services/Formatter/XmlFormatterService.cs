using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class XMLFormatterService : IFormatter
{
    public string Format(string customer, List<PerformanceResult> performances, int totalAmount, int credits)
    {
        var statement = new XElement("Statement",
            new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
            new XElement("Customer", customer),
            new XElement("Items",
                from performance in performances
                select new XElement("Item",
                    new XElement("AmountOwed", FormatAmount(performance.Amount)),
                    new XElement("EarnedCredits", performance.Credits),
                    new XElement("Seats", performance.Audience)
                )
            ),
            new XElement("AmountOwed", FormatAmount(totalAmount)),
            new XElement("EarnedCredits", credits)
        );

        var declaration = new XDeclaration("1.0", "utf-8", null);

        var document = new XDocument(
            declaration,
            statement
        );

        return document.Declaration.ToString() + "\n" + document.ToString();
    }

    private static string FormatAmount(int amount)
    {
        return (amount / 100.0).ToString("0.##");
    }
}
