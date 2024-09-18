using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Calculatros;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Utilities;
using TheatricalPlayersRefactoringKata.XmlModels;

public class XmlFormatter
{
    public string GenerateXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statement = new Statement
        {
            Customer = invoice.Customer,
            AmountOwed = 0,
            EarnedCredits = 0
        };

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = new PlayAmountCalculator().Calculate(perf, play);
            var item = new Item
            {
                AmountOwed = Convert.ToDecimal(thisAmount / 100M),
                EarnedCredits = Math.Max(perf.Audience - 30, 0) + ("comedy" == play.Type ? (int)Math.Floor((decimal)perf.Audience / 5) : 0),
                Seats = perf.Audience
            };

            statement.Items.Add(item);
            statement.AmountOwed += item.AmountOwed;
            statement.EarnedCredits += item.EarnedCredits;
        }

        var serializer = XmlSerializerUtility.CreateSerializer<Statement>();

        using (var memoryStream = new MemoryStream())
        {
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                serializer.Serialize(streamWriter, statement);
                streamWriter.Flush();
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}
