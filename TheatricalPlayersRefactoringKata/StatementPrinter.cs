using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using TheatricalPlayersRefactoringKata.Strategies;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly Dictionary<string, IPlayStrategy> _strategies;

    public StatementPrinter()
    {
        _strategies = new Dictionary<string, IPlayStrategy>
        {
            { "tragedy", new TragedyPlayStrategy() },
            { "comedy", new ComedyPlayStrategy() },
            { "history", new HistoryPlayStrategy() }
        };
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var strategy = _strategies[play.Type];

            double thisAmount = strategy.CalculateAmount(perf.Audience, play.Lines);
            volumeCredits += strategy.CalculateCredits(perf.Audience);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        var volumeCredits = 0;
        var xmlDocument = new XmlDocument();
        var root = xmlDocument.CreateElement("statement");
        xmlDocument.AppendChild(root);

        var customerElement = xmlDocument.CreateElement("customer");
        customerElement.InnerText = invoice.Customer;
        root.AppendChild(customerElement);

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var strategy = _strategies[play.Type];

            double thisAmount = strategy.CalculateAmount(perf.Audience, play.Lines);
            volumeCredits += strategy.CalculateCredits(perf.Audience);

            var performanceElement = xmlDocument.CreateElement("performance");
            performanceElement.SetAttribute("play", play.Name);
            performanceElement.SetAttribute("amount", Convert.ToDecimal(thisAmount / 100).ToString("C", CultureInfo.InvariantCulture));
            performanceElement.SetAttribute("audience", perf.Audience.ToString());
            root.AppendChild(performanceElement);

            totalAmount += thisAmount;
        }

        var totalAmountElement = xmlDocument.CreateElement("totalAmount");
        totalAmountElement.InnerText = Convert.ToDecimal(totalAmount / 100).ToString("C", CultureInfo.InvariantCulture);
        root.AppendChild(totalAmountElement);

        var volumeCreditsElement = xmlDocument.CreateElement("volumeCredits");
        volumeCreditsElement.InnerText = volumeCredits.ToString();
        root.AppendChild(volumeCreditsElement);

        return BeautifyXml(xmlDocument);
    }

    private string BeautifyXml(XmlDocument doc)
    {
        StringBuilder sb = new StringBuilder();
        using (var writer = XmlWriter.Create(sb, new XmlWriterSettings { Indent = true }))
        {
            doc.Save(writer);
        }
        return sb.ToString();
    }
}
