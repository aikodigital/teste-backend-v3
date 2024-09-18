using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

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
            decimal lines = play.Lines;

            lines = Math.Max(1000,Math.Min(lines,4000)); // between 1000 and 4000 only

            decimal thisAmount = lines/10; 

            // calculate amount based on play type
            var calcPlayAmount = PlayCalculatorFactory.createCalculator(play.Type); 
            thisAmount = calcPlayAmount.calculateAmount(perf, thisAmount);

            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type)
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);
            totalAmount += thisAmount;
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCreditsTotal = 0;

        // Cria o modelo para o XML
        Statement statement = new Statement
        {
            Customer = invoice.Customer,
            Items = new List<StatementItem>()
        };

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            decimal lines = play.Lines;

            lines = Math.Max(1000, Math.Min(lines, 4000));
            decimal thisAmount = lines / 10;

            var calcPlayAmount = PlayCalculatorFactory.createCalculator(play.Type);
            thisAmount = calcPlayAmount.calculateAmount(perf, thisAmount);

            // Set Credits
            var volumeCredits = Math.Max(perf.Audience - 30, 0);
            if (play.Type.Equals("comedy"))
            {
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            }

            volumeCreditsTotal += volumeCredits;

            // add to XML
            statement.Items.Add(new StatementItem
            {
                AmountOwed = thisAmount,
                EarnedCredits = volumeCredits,
                Seats = perf.Audience
            });

            totalAmount += thisAmount;
        }

        statement.AmountOwed = totalAmount;
        statement.EarnedCredits = volumeCreditsTotal;

        // Serializa o objeto Statement em XML
        var xmlSerializer = new XmlSerializer(typeof(Statement));
        using (var stringWriter = new Utf8StringWriter())
        {
            xmlSerializer.Serialize(stringWriter, statement);
            return stringWriter.ToString();
        }
    }

    private sealed class Utf8StringWriter : StringWriter
    {
        // write utf-8 string on XMLWriter
        public override Encoding Encoding => Encoding.UTF8;
    }

}
