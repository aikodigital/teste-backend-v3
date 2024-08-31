using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays, OutputType outputType)
        {
            switch (outputType)
            {
                case OutputType.XML:
                    return PrintXml(invoice, plays).ToString();
                case OutputType.JSON:
                    return PrintJson(invoice, plays);
                case OutputType.TXT:
                    return PrintTxt(invoice, plays);
                default:
                    throw new ArgumentException("Unsupported output type");
            }
        }

        private string PrintTxt(Invoice invoice, Dictionary<string, Play> plays)
        {
            double totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                Play p = new Play(play.Name, play.Lines, play.Type, PlayStrategyFactory.CreateStrategy(play.Type));
                var lines = play.Lines;

                if (lines < 1000) lines = 1000;
                if (lines > 4000) lines = 4000;
                double thisAmount = 0;
                thisAmount = p.CalculateAmount(new Performance(perf.PlayId, perf.Audience), thisAmount, lines);

                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                // print line for this order
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        private XDocument PrintXml(Invoice invoice, Dictionary<string, Play> plays)
        {
            double totalAmount = 0;
            var volumeCredits = 0;
            var statement = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items")
            );

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                Play p = new Play(play.Name, play.Lines, play.Type, PlayStrategyFactory.CreateStrategy(play.Type));
                var lines = play.Lines;

                if (lines < 1000) lines = 1000;
                if (lines > 4000) lines = 4000;
                double thisAmount = 0;
                thisAmount = p.CalculateAmount(new Performance(perf.PlayId, perf.Audience), thisAmount, lines);

                var earnedCredits = Math.Max(perf.Audience - 30, 0);
                if ("comedy" == play.Type) earnedCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                volumeCredits += earnedCredits;

                var item = new XElement("Item",
                    new XElement("AmountOwed", thisAmount / 100),
                    new XElement("EarnedCredits", earnedCredits),
                    new XElement("Seats", perf.Audience)
                );

                statement.Element("Items")?.Add(item);
                totalAmount += thisAmount;
            }

            statement.Add(new XElement("AmountOwed", totalAmount / 100));
            statement.Add(new XElement("EarnedCredits", volumeCredits));

            return new XDocument(new XDeclaration("1.0", "utf-8", "yes"), statement);
        }

        private string PrintJson(Invoice invoice, Dictionary<string, Play> plays)
        {
            double totalAmount = 0;
            var volumeCredits = 0;
            var items = new List<object>();

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                Play p = new Play(play.Name, play.Lines, play.Type, PlayStrategyFactory.CreateStrategy(play.Type));
                var lines = play.Lines;

                if (lines < 1000) lines = 1000;
                if (lines > 4000) lines = 4000;
                double thisAmount = 0;
                thisAmount = p.CalculateAmount(new Performance(perf.PlayId, perf.Audience), thisAmount, lines);

                var earnedCredits = Math.Max(perf.Audience - 30, 0);
                if ("comedy" == play.Type) earnedCredits += (int)Math.Floor((decimal)perf.Audience / 5);
                volumeCredits += earnedCredits;

                items.Add(new
                {
                    AmountOwed = thisAmount / 100,
                    EarnedCredits = earnedCredits,
                    Seats = perf.Audience
                });

                totalAmount += thisAmount;
            }

            var result = new
            {
                Customer = invoice.Customer,
                Items = items,
                AmountOwed = totalAmount / 100,
                EarnedCredits = volumeCredits
            };

            return JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
