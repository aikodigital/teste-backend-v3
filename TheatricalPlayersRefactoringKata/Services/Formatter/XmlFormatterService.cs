using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class XMLFormatterService : IFormatter
    {
        public string Format(string customer, List<PerformanceResult> performances, int totalAmount, int credits)
        {
            // Cria o elemento raiz do XML

            var statement = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", customer),
                new XElement("Items",
                    performances.Select(performance =>
                        new XElement("Item",
                            new XElement("AmountOwed", (performance.Amount / 100m).ToString("F2")), // Converte de centavos para valor decimal
                            new XElement("EarnedCredits", CalculateEarnedCredits(performance.Audience)),
                            new XElement("Seats", performance.Audience)
                        )
                    )
                ),
                new XElement("AmountOwed", (totalAmount / 100m).ToString("F2")),
                new XElement("EarnedCredits", credits)
            );

            return statement.ToString();
        }

        private int CalculateEarnedCredits(int audience)
        {
            // Cria a regra de cálculo de créditos base
            return audience > 30 ? audience - 30 : 0;
        }
    }
}
