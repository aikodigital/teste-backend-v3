using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using System.Linq;
using TheatricalPlayersRefactoringKata.Core.Services;

namespace TheatricalPlayersRefactoringKata.Infrastructure
{
    public class XmlStatementGenerator : IStatementGenerator
    {
        public string Generate(Invoice invoice, Dictionary<Guid, Play> plays)
        {
            if (!invoice.Performances.Any())
            {
                Console.WriteLine("A fatura não contém performances.");
                return string.Empty;
            }

            var statement = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items",
                    invoice.Performances.Select(performance =>
                    {
                        var amountOwed = CalculateAmountOwed(performance, plays);
                        var earnedCredits = CalculateCredits(performance, plays);

                        Console.WriteLine($"Performance: {performance.PlayId}, Amount Owed: {amountOwed}, Earned Credits: {earnedCredits}, Audience: {performance.Audience}");

                        return new XElement("Item",
                            new XElement("AmountOwed", amountOwed),
                            new XElement("EarnedCredits", earnedCredits),
                            new XElement("Seats", performance.Audience)
                        );
                    })
                )
            );

            return statement.ToString();
        }

        private decimal CalculateAmountOwed(Performance performance, Dictionary<Guid, Play> plays)
        {
            if (plays.TryGetValue(performance.PlayId, out var play))
            {
                return play.Price;
            }

            Console.WriteLine($"Play not found for PlayId: {performance.PlayId}");
            return 0;
        }

        private int CalculateCredits(Performance performance, Dictionary<Guid, Play> plays)
        {
            if (plays.TryGetValue(performance.PlayId, out var play))
            {
                var calculator = new ComedyCalculator(); 
                return calculator.CalculateCredits(performance);
            }

            Console.WriteLine($"Play not found for PlayId: {performance.PlayId}");
            return 0;
        }
    }
}