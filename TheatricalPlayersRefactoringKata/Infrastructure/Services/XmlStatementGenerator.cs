using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure
{
    public class XmlStatementGenerator : IStatementGenerator
    {
        private readonly Dictionary<string, IPerformanceCalculator> _calculators;

        public XmlStatementGenerator(IEnumerable<IPerformanceCalculator> calculators)
        {
            _calculators = calculators.ToDictionary(c => c.GetType().Name.Replace("Calculator", ""), c => c);
        }

        public string Generate(Invoice invoice, Dictionary<Guid, Play> plays)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice), "O parâmetro 'invoice' não pode ser nulo.");

            if (plays == null)
                throw new ArgumentNullException(nameof(plays), "O parâmetro 'plays' não pode ser nulo.");

            var totalAmount = 0m;
            var totalCredits = 0;

            var root = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer)
            );

            var itemsElement = new XElement("Items");

            foreach (var perf in invoice.Performances)
            {
                if (!plays.TryGetValue(perf.PlayId, out var play))
                    throw new KeyNotFoundException($"A peça com o ID '{perf.PlayId}' não foi encontrada.");

                if (!_calculators.TryGetValue(play.Type.ToString(), out var calculator))
                    throw new ArgumentException($"Nenhum calculador disponível para o tipo de peça: {play.Type}");

                var amount = calculator.CalculatePrice(perf);
                var credits = calculator.CalculateCredits(perf);

                totalAmount += amount;
                totalCredits += credits;

                itemsElement.Add(new XElement("Item",
                    new XElement("AmountOwed", amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)),
                    new XElement("EarnedCredits", credits),
                    new XElement("Seats", perf.Audience)
                ));
            }

            root.Add(new XElement("AmountOwed", totalAmount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)));
            root.Add(new XElement("EarnedCredits", totalCredits));
            root.Add(itemsElement);

            return root.ToString();
        }
    }
}