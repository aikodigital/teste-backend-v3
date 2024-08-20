using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{

    // <summary>
    /// Generates a detailed report for an invoice, including the total amount and accumulated credits.
    /// The report includes the name of the play, the formatted play value, and the number of seats for each performance.
    /// It also calculates and displays the total invoice amount and total credits earned.
    /// </summary>
    /// <param name="invoice">
    /// The invoice containing information about the client and the performances.
    /// </param>
    /// <param name="plays">
    /// A dictionary that maps part IDs to objects <see cref="Play"/>.
    /// </param>
    /// <returns>
    /// A formatted string representing the invoice report, including performance details,
    /// the total invoice value and the total credits earned.
    /// </returns>
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
                play.Audience = perf.Audience; 
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

    /// <summary>
    /// Generates a report in XML format for an invoice, including performance details, 
    /// the total amount and accumulated credits. The report is structured in XML and includes 
    /// information such as the amount due for each item, the credits earned and the number of seats.
    /// </summary>
    /// <param name="invoice">
    /// The invoice containing information about the client and the performances.
    /// </param>
    /// <param name="plays">
    /// A dictionary that maps part IDs to objects <see cref="Play"/>.
    /// </param>
    /// <returns>
    /// A string representing the invoice report in XML format, including performance details,
    /// the total invoice value and the total credits earned.
    /// </returns>
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
