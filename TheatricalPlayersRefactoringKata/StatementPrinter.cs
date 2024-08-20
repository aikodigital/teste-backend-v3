using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];

            if (play is Historic historicPlay)
            {
                historicPlay.SetAudience(perf.Audience);
            }
            else
            {
                play.Audience = perf.Audience;  // Set the audience for calculation
            }

            var thisAmount = play.CalculatorValor();
            var playCredits = play.CalculatorCredits();
            volumeCredits += playCredits;

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);
            totalAmount += Convert.ToDecimal(thisAmount);
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string PrintXML(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        int totalCredits = 0;

        var items = new XElement("Items");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];

            if (play is Historic historicPlay)
            {
                historicPlay.SetAudience(perf.Audience);
            }
            else
            {
                play.Audience = perf.Audience;
            }

            var thisAmount = play.CalculatorValor();
            var playCredits = play.CalculatorCredits();

            totalAmount += (decimal)thisAmount;
            totalCredits += playCredits;

            items.Add(new XElement("Item",
            new XElement("AmountOwed", Convert.ToString(Convert.ToDecimal(thisAmount), CultureInfo.InvariantCulture)),
            new XElement("EarnedCredits", playCredits),
            new XElement("Seats", perf.Audience)
        ));
        }

        var statement = new XElement("Statement",

            new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
            new XElement("Customer", invoice.Customer),
            items,
            new XElement("AmountOwed", totalAmount.ToString("0.0", CultureInfo.InvariantCulture)),
            new XElement("EarnedCredits", totalCredits)
        );

        var xmlDocument = new XDocument(
        new XDeclaration("1.0", "utf-8", null),
        statement
    );


        using (var sw = new Utf8StringWriter())
        {
            xmlDocument.Save(sw);
            return sw.ToString();
        }
    }


    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

}
