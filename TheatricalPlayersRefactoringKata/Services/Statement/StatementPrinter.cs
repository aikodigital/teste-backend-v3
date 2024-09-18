using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.enums;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{   
    public static string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10;
            var _tragedy = thisAmount;
            var _comedy = thisAmount;  
            switch (play.Type) 
            {
                case PlayType.tragedy:
                    if (perf.Audience > 30) 
                    {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case PlayType.comedy:
                    if (perf.Audience > 20) 
                    {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                case PlayType.history:   
                               
                    if (perf.Audience > 30) 
                    {
                        _tragedy += 1000 * (perf.Audience - 30);
                    }

                    if (perf.Audience > 20) 
                    {
                        _comedy += 10000 + 500 * (perf.Audience - 20);
                    }
                     _comedy += 300 * perf.Audience;

                     thisAmount = _tragedy + _comedy;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ( play.Type.Equals(PlayType.comedy)) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount )/ 100, perf.Audience);
            totalAmount += thisAmount;
        }
        


        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount )/ 100);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public static string PrintStatement(Statement statement)
    {
        string result = string.Empty;
        XmlSerializer serializer = new XmlSerializer(typeof(Statement));

        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Making the patter to UTF8 with BOM
            using (StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                serializer.Serialize(streamWriter, statement);
                result = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
        return result;
    }
}


