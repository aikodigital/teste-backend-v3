using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var thisAmount = play.CalculateBaseAmount();

            IPlayAmountCalculator calculator = PlayAmountCalculatorFactory.GetCalculator(play.Type);
            thisAmount = calculator.CalculateAmount(perf, thisAmount);

            volumeCredits += play.CalculateVolumeCredits(perf.Audience);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100m), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100m));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;

        using MemoryStream memoryStream = new();
        XmlWriterSettings settings = new()
        {
            Indent = true,
            Encoding = Encoding.UTF8
        };

        using (XmlWriter writer = XmlWriter.Create(memoryStream, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("Statement");
            writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
            writer.WriteElementString("Customer", invoice.Customer);
            writer.WriteStartElement("Items");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var thisAmount = play.CalculateBaseAmount();

                IPlayAmountCalculator calculator = PlayAmountCalculatorFactory.GetCalculator(play.Type);
                thisAmount = calculator.CalculateAmount(perf, thisAmount);

                var earnedCredits = play.CalculateVolumeCredits(perf.Audience);
                volumeCredits += earnedCredits;

                totalAmount += thisAmount;

                writer.WriteStartElement("Item");
                writer.WriteElementString("AmountOwed", Convert.ToDecimal(thisAmount / 100m).ToString(CultureInfo.InvariantCulture));
                writer.WriteElementString("EarnedCredits", earnedCredits.ToString(CultureInfo.InvariantCulture));
                writer.WriteElementString("Seats", perf.Audience.ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteElementString("AmountOwed", Convert.ToDecimal(totalAmount / 100m).ToString(CultureInfo.InvariantCulture));
            writer.WriteElementString("EarnedCredits", volumeCredits.ToString(CultureInfo.InvariantCulture));

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        byte[] xmlBytes = memoryStream.ToArray();

        return Encoding.UTF8.GetString(xmlBytes);
    }
}
