using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Application.Factory;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Application.StatementPrinter;

public class StatementPrinter : IStatementPrinter
{
    public string TextPrint(Invoice invoice, Dictionary<string, Domain.Entity.Play> plays)
    {
        decimal totalAmount = 0;
        decimal volumeCredits = 0;

        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;

            decimal thisAmount = (decimal)lines / 10;

            var playGenre = GenreFactory.CreatePlay(play.Name, lines, play.Type);

            thisAmount = playGenre.CalculateAmount(perf.Audience);
            volumeCredits += playGenre.CalculateCredits(perf.Audience);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
            totalAmount += thisAmount;
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string XmlPrint(Invoice invoice, Dictionary<string, Domain.Entity.Play> plays)
    {
        var root = new XElement("Statement",
            new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
            new XElement("Customer", invoice.Customer),
            new XElement("Items")
        );

        decimal totalAmount = 0;
        decimal volumeCredits = 0;

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;

            decimal thisAmount = lines / 10m;
            
            var playGenre = GenreFactory.CreatePlay(play.Name, lines, play.Type);

            thisAmount = playGenre.CalculateAmount(perf.Audience);
            int currentCredits = (int)playGenre.CalculateCredits(perf.Audience);

            root.Element("Items").Add(
                new XElement("Item",
                    new XElement("AmountOwed",
                        thisAmount % 1 == 0
                            ? ((int)thisAmount).ToString()
                            : thisAmount.ToString("F1", CultureInfo.InvariantCulture)),
                    new XElement("EarnedCredits", currentCredits),
                    new XElement("Seats", perf.Audience)
                )
            );

            volumeCredits += currentCredits;
            totalAmount += thisAmount;
        }

        root.Add(
            new XElement("AmountOwed",
                totalAmount % 1 == 0
                    ? ((int)totalAmount).ToString()
                    : totalAmount.ToString("F1", CultureInfo.InvariantCulture)),
            new XElement("EarnedCredits", volumeCredits)
        );

        var xmlDeclaration = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
        return xmlDeclaration + root;
    }
}
