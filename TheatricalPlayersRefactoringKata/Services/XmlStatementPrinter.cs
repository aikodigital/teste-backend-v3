using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class XmlStatementPrinter
    {
        private readonly StatementCalculator _statementCalculator;

        public XmlStatementPrinter(StatementCalculator statementCalculator)
        {
            _statementCalculator = statementCalculator;
        }

        public string Print(Invoice invoice, Dictionary<int, Play> plays)
        {
            var items = new List<XElement>();
            decimal totalAmountOwed = 0;
            int totalEarnedCredits = 0;

            foreach (var performance in invoice.Performances)
            {
                var play = plays[performance.PlayId];
                var amountOwed = _statementCalculator.CalculateAmount(play, performance.Seats);
                var earnedCredits = _statementCalculator.CalculatePoints(play, performance.Seats);

                items.Add(new XElement("Item",
                    new XElement("AmountOwed", amountOwed),
                    new XElement("EarnedCredits", earnedCredits),
                    new XElement("Seats", performance.Seats)
                ));

                totalAmountOwed += amountOwed;
                totalEarnedCredits += earnedCredits;
            }

            var doc = new XDocument(
                new XElement("Statement",
                    new XElement("Customer", invoice.Customer),
                    new XElement("Items", items),
                    new XElement("AmountOwed", totalAmountOwed),
                    new XElement("EarnedCredits", totalEarnedCredits)
                )
            );

            using (var writer = new StringWriter())
            {
                doc.Save(writer);
                return writer.ToString();
            }
        }
    }
}
