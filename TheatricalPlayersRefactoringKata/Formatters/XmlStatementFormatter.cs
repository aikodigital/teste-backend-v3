using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    public class XmlStatementFormatter : IStatementFormatter
    {
        public string Format(Invoice invoice, Dictionary<string, Play> plays, Dictionary<string, IStatementStrategy> strategies)
        {
            var items = new List<XElement>();
            decimal totalAmount = 0;
            int volumeCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];

                if (!strategies.TryGetValue(play.Type, out var strategy))
                {
                    throw new Exception("Unknown play type: " + play.Type);
                }

                // Cálculo do preço e créditos
                var thisAmount = strategy.CalculatePrice(play, perf) / 100m; // Converter para unidades monetárias
                var credits = strategy.CalculateCredits(play, perf);
                volumeCredits += credits;

                // Adicionando o item ao XML, formatando corretamente o valor monetário
                items.Add(new XElement("Item",
                    new XElement("AmountOwed", thisAmount.ToString("0.##", CultureInfo.InvariantCulture)), // Remove zeros desnecessários
                    new XElement("EarnedCredits", credits),
                    new XElement("Seats", perf.Audience)
                ));

                totalAmount += thisAmount;
            }

            // Criação do documento XML com a declaração e namespaces corretos
            var document = new XDocument(
                new XElement("Statement",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                    new XElement("Customer", invoice.Customer),
                    new XElement("Items", items),
                    new XElement("AmountOwed", totalAmount.ToString("0.##", CultureInfo.InvariantCulture)), // Formatação correta
                    new XElement("EarnedCredits", volumeCredits)
                )
            );

            // Convertendo o documento para string e adicionando a declaração XML manualmente
            var xmlString = document.ToString(SaveOptions.None);
            var xmlDeclaration = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            return xmlDeclaration + xmlString;
        }
    }
}
