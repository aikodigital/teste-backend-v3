using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata
{
    public class XmlStatementPrinter
    {
        public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var root = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items")
            );

            decimal totalAmount = 0;
            var volumeCredits = 0;
            var cultureInfo = CultureInfo.InvariantCulture;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var lines = play.Lines;

                if (lines < 1000) lines = 1000;
                if (lines > 4000) lines = 4000;

                decimal thisAmount = lines / 10m;

                switch (play.Type)
                {
                    case "tragedy":
                        thisAmount = CalculateTragedyAmount(lines, perf.Audience);
                        break;

                    case "comedy":
                        thisAmount = CalculateComedyAmount(lines, perf.Audience);
                        break;

                    case "history":
                        var tragedyAmount = CalculateTragedyAmount(lines, perf.Audience);
                        var comedyAmount = CalculateComedyAmount(lines, perf.Audience);
                        thisAmount = tragedyAmount + comedyAmount;
                        break;

                    default:
                        throw new Exception("unknown type: " + play.Type);
                }

                int currentCredits = Math.Max(perf.Audience - 30, 0);
                if (play.Type == "comedy")
                {
                    currentCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                }

                root.Element("Items").Add(
                    new XElement("Item",
                    new XElement("AmountOwed", thisAmount % 1 == 0 ? ((int)thisAmount).ToString() : thisAmount.ToString("F1", CultureInfo.InvariantCulture)),
                    new XElement("EarnedCredits", currentCredits),
                    new XElement("Seats", perf.Audience)
                    )
                );

                volumeCredits += currentCredits;
                totalAmount += thisAmount;
            }

            root.Add(
                new XElement("AmountOwed", totalAmount % 1 == 0 ? ((int)totalAmount).ToString() : totalAmount.ToString("F1", CultureInfo.InvariantCulture)),
                new XElement("EarnedCredits", volumeCredits)
            );

            // Adiciona a declaração XML ao início da string
            var xmlDeclaration = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            return xmlDeclaration + root.ToString();
        }

        private decimal CalculateTragedyAmount(int lines, int audience)
        {
            decimal amount = lines / 10m;
            if (audience > 30)
            {
                amount += 10m * (audience - 30);
            }
            return amount;
        }

        private decimal CalculateComedyAmount(int lines, int audience)
        {
            decimal amount = lines / 10m + 3m * audience;
            if (audience > 20)
            {
                amount += 100m + 5m * (audience - 20);
            }
            return amount;
        }
    }
}
