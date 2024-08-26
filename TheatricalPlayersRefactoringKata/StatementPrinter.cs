using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0M;
            var volumeCredits = 0;
            var result = new StringBuilder();
            var cultureInfo = new CultureInfo("en-US");

            result.AppendFormat("Statement for {0}\n", invoice.Customer);

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var lines = play.Lines;
                if (lines < 1000) lines = 1000;
                if (lines > 4000) lines = 4000;
                var thisAmount = lines * 10M;
                switch (play.Type)
                {
                    case "tragedy":
                        thisAmount = CalculateTragedyAmount(lines, perf.Audience);
                        break;
                    case "comedy":
                        thisAmount = CalculateComedyAmount(lines, perf.Audience);
                        break;
                    case "history":
                        thisAmount = CalculateHistoryAmount(lines, perf.Audience);
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                // print line for this order
                result.AppendFormat(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
                totalAmount += thisAmount;
            }
            result.AppendFormat(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
            result.AppendFormat("You earned {0} credits\n", volumeCredits);
            return result.ToString();
        }

        public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var doc = new XDocument(new XElement("Statement",
                new XElement("Customer", invoice.Customer),
                new XElement("TotalAmount", "0"),
                new XElement("VolumeCredits", "0"),
                new XElement("Performances")
            ));

            var totalAmount = 0M;
            var volumeCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var lines = play.Lines;
                if (lines < 1000) lines = 1000;
                if (lines > 4000) lines = 4000;
                var thisAmount = lines * 10M;
                switch (play.Type)
                {
                    case "tragedy":
                        thisAmount = CalculateTragedyAmount(lines, perf.Audience);
                        break;
                    case "comedy":
                        thisAmount = CalculateComedyAmount(lines, perf.Audience);
                        break;
                    case "history":
                        thisAmount = CalculateHistoryAmount(lines, perf.Audience);
                        break;
                    default:
                        throw new Exception("unknown type: " + play.Type);
                }
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                totalAmount += thisAmount;

                doc.Root.Element("Performances").Add(new XElement("Performance",
                    new XElement("Name", play.Name),
                    new XElement("Amount", FormatCurrency(thisAmount)),
                    new XElement("Seats", perf.Audience)
                ));
            }

            doc.Root.Element("TotalAmount").Value = FormatCurrency(totalAmount);
            doc.Root.Element("VolumeCredits").Value = volumeCredits.ToString();

            return doc.ToString();
        }

        private string FormatCurrency(decimal amount)
        {
            var cultureInfo = new CultureInfo("en-US");
            return amount.ToString("C", cultureInfo);
        }
        private decimal CalculateTragedyAmount(int lines, int audience)
        {
            var baseAmount = lines * 10M;
            var extraAmount = audience > 30 ? 1000M * (audience - 30) : 0M;
            return (baseAmount + extraAmount) / 100;
        }

        private decimal CalculateComedyAmount(int lines, int audience)
        {
            var baseAmount = lines * 10M;
            var extraAmount = audience > 20 ? 10000M + 500M * (audience - 20) : 0M;
            return (baseAmount + extraAmount + 300M * audience) / 100;
        }

        private decimal CalculateHistoryAmount(int lines, int audience)
        {
            return CalculateTragedyAmount(lines, audience) + CalculateComedyAmount(lines, audience);
        }
    }
}
