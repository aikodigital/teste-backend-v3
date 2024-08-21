using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Models;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    public static IPlayCalculator GetCalculatorByType(string playType) => playType.ToLower() switch
    {
        "tragedy" => new TragedyCalculator(),
        "comedy" => new ComedyCalculator(),
        "history" => new HistoryCalculator(),
        _ => throw new Exception("unknown type: " + playType),
    };

    public string PrintText(Invoice invoice, Dictionary<string, Play> plays)
    {
        var billingStatement = new StringBuilder($"Statement for {invoice.Customer}\n");
        try
        {
            decimal totalAmount = 0m;
            decimal volumeCredits = 0;
            CultureInfo cultureInfo = new CultureInfo("en-US");
            IPlayCalculator playTypeCalculator;

            foreach (Performance perf in invoice.Performances)
            {
                var play = plays.FirstOrDefault(p => p.Key.ToLower() == perf.PlayId).Value;

                if (play is null) throw new ArgumentOutOfRangeException($"{perf.PlayId} is not a valid Play.");

                playTypeCalculator = GetCalculatorByType(play.Type);
                decimal thisAmount = playTypeCalculator.CalculateAmount(play, perf.Audience);
                volumeCredits += playTypeCalculator.CalculateCredits(play, perf.Audience);

                billingStatement.AppendLine(cultureInfo, $"  {play.Name}: {thisAmount:C} ({perf.Audience} seats)");
                totalAmount += thisAmount;
            }
            billingStatement.AppendLine(cultureInfo, $"Amount owed is {totalAmount:C}");
            billingStatement.AppendLine($"You earned {volumeCredits} credits");
        }
        catch { throw; }
        return billingStatement.ToString();
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        try
        {
            decimal totalAmount = 0m;
            decimal volumeCredits = 0;

            XElement[] items = invoice.Performances.Select(perf =>
            {
                Play? play = plays.FirstOrDefault(p => p.Key.ToLower() == perf.PlayId.ToLower()).Value;

                if (play is null) throw new ArgumentOutOfRangeException($"{perf.PlayId} is not a valid Play.");

                var playTypeCalculator = GetCalculatorByType(play.Type);
                decimal thisAmount = playTypeCalculator.CalculateAmount(play, perf.Audience);
                decimal thisCredits = playTypeCalculator.CalculateCredits(play, perf.Audience);

                totalAmount += thisAmount;
                volumeCredits += thisCredits;

                return new XElement("Item",
                           new XElement("AmountOwed", thisAmount),
                           new XElement("EarnedCredits", thisCredits),
                           new XElement("Seats", perf.Audience));
            }).ToArray();

            var xmlDoc = new XDocument(
                             new XDeclaration("1.0", "UTF-8", null),
                             new XElement("Statement",
                                 new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                                 new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                                 new XElement("Customer", invoice.Customer),
                                 new XElement("Items", items),
                                 new XElement("AmountOwed", totalAmount),
                                 new XElement("EarnedCredits", volumeCredits)
                                        )
                            );
            
            string xmlString = string.Empty;
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                xmlDoc.Save(writer);
                xmlString = Encoding.UTF8.GetString(stream.ToArray());
            }
            return xmlString;
        }
        catch { throw; }
    }
}
