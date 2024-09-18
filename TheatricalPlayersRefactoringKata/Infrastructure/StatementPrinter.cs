using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Application;

namespace TheatricalPlayersRefactoringKata.Infrastructure;

public class StatementPrinter
{
    private readonly StatementService _statementService;

    public StatementPrinter(StatementService statementService)
    {
        _statementService = statementService;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10;
            switch (play.Type)
            {
                case "tragedy":
                    if (perf.Audience > 30)
                    {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    if (perf.Audience > 20)
                    {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                case "history":
                    // Calcula o valor como tragédia
                    var tragedyAmount = thisAmount;
                    if (perf.Audience > 30)
                    {
                        tragedyAmount += 1000 * (perf.Audience - 30);
                    }

                    // Calcula o valor como comédia
                    var comedyAmount = thisAmount;
                    comedyAmount += 300 * perf.Audience;
                    if (perf.Audience > 20)
                    {
                        comedyAmount += 10000 + 500 * (perf.Audience - 20);
                    }

                    // Valor total da peça histórica
                    thisAmount = tragedyAmount + comedyAmount;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount) / 100, perf.Audience);
            totalAmount += thisAmount;
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount) / 100);
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string PrintTxt(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statementData = _statementService.GenerateStatementData(invoice, plays);

        var result = string.Format("Statement for {0}\n", statementData.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var item in statementData.Items)
        {
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", item.PlayName, item.AmountOwed, item.Seats);
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", statementData.TotalAmountOwed);
        result += string.Format("You earned {0} credits\n", statementData.TotalEarnedCredits);
        return result;
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statementData = _statementService.GenerateStatementData(invoice, plays);

        var serializer = new XmlSerializer(typeof(StatementData));
        using (var stringWriter = new StringWriter())
        {
            using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                serializer.Serialize(writer, statementData);
            }

            // Converte a saída para UTF-8
            string xmlUtf16 = stringWriter.ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(xmlUtf16);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
