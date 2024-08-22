using System.Collections.Generic;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata;

public class XmlStatementPrinter
{
    private readonly Dictionary<string, ICalculatorStrategy> _calculatorStrategies;

    public XmlStatementPrinter()
    {
        _calculatorStrategies = new Dictionary<string, ICalculatorStrategy>
        {
            { "tragedy", new TragedyCalculator() },
            { "comedy", new ComedyCalculator() },
            { "historical", new HistoricalCalculator() }
        };
    }

    public XDocument Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0m;
        var totalCredits = 0;
        var performances = new XElement("Performances");

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var calculator = _calculatorStrategies[play.Type];
            var amount = calculator.CalculateAmount(performance, play);
            var credits = calculator.CalculateCredits(performance, play);

            totalAmount += amount;
            totalCredits += credits;

            var performanceElement = new XElement("Performance",
                new XElement("PlayName", play.Name),
                new XElement("Amount", amount),
                new XElement("Audience", performance.Audience)
            );
            performances.Add(performanceElement);
        }

        var invoiceElement = new XElement("Invoice",
            new XElement("Customer", invoice.Customer),
            performances,
            new XElement("TotalAmount", totalAmount),
            new XElement("TotalCredits", totalCredits)
        );

        return new XDocument(invoiceElement);
    }
}
