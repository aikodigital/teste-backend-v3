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
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = new StringBuilder();
            result.AppendFormat("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new("en-US");

            foreach (var performance in invoice.Performances)
            {
                var play = plays[performance.PlayId];
                IChargeStrategy strategy = GetChargeStrategy(play.Type);

                var thisAmount = strategy.CalculateBilling(performance, play);
                var thisCredits = strategy.CalculateCredits(performance);

                // Add volume credits
                volumeCredits += thisCredits;

                // Print line for this order
                result.AppendFormat(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), performance.Audience);
                totalAmount += thisAmount;
            }

            result.AppendFormat(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result.AppendFormat("You earned {0} credits\n", volumeCredits);
            return result.ToString();
        }

        public XDocument PrintXml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var root = new XElement("Statement",
                new XElement("Customer", invoice.Customer),
                new XElement("TotalAmount", GetFormattedAmount(invoice, plays)),
                new XElement("TotalCredits", GetTotalCredits(invoice, plays))
            );

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                IChargeStrategy strategy = GetChargeStrategy(play.Type);

                var thisAmount = strategy.CalculateBilling(perf, play);
                var thisCredits = strategy.CalculateCredits(perf);

                root.Add(new XElement("Performance",
                    new XElement("Play", play.Name),
                    new XElement("Amount", $"{thisAmount / 100:C}"),
                    new XElement("Seats", perf.Audience)
                ));
            }

            // Add the total amount and credits at the end of the XML for completeness
            root.Add(new XElement("AmountOwed", $"{GetTotalAmount(invoice, plays) / 100:C}"));
            root.Add(new XElement("CreditsEarned", GetTotalCredits(invoice, plays)));

            return new XDocument(root);
        }

        private string GetFormattedAmount(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;

            foreach (var performance in invoice.Performances)
            {
                var play = plays[performance.PlayId];
                IChargeStrategy strategy = GetChargeStrategy(play.Type);
                totalAmount += strategy.CalculateBilling(performance, play);
            }
            return $"{totalAmount / 100:C}";
        }

        private int GetTotalCredits(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                IChargeStrategy strategy = GetChargeStrategy(play.Type);
                totalCredits += strategy.CalculateCredits(perf);
            }
            return totalCredits;
        }

        private int GetTotalAmount(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;

            foreach (var performance in invoice.Performances)
            {
                var play = plays[performance.PlayId];
                IChargeStrategy strategy = GetChargeStrategy(play.Type);
                totalAmount += strategy.CalculateBilling(performance, play);
            }
            return totalAmount;
        }

        private static IChargeStrategy GetChargeStrategy(string playType)
        {
            return playType switch
            {
                "tragedy" => new TragedyCharge(),
                "comedy" => new CollectionChargy(),
                "history" => new HistoryCharge(),
                _ => throw new Exception("unknown type: " + playType),
            };
        }
    }
}
