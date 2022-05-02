using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    int minLines = 1000;
    int maxLines = 4000;
    CultureInfo cultureInfo = new CultureInfo("en-US");

    public string Print(Invoice invoice, Dictionary<string, Play> plays, string format)
    {
        PrintData printData = new PrintData();
        var totalAmount = 0;
        var volumeCredits = 0;

        printData.Customer = invoice.Customer;
        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < minLines) lines = minLines;
            if (lines > maxLines) lines = maxLines;
            var thisAmount = play.Type.CalcAmount(perf, lines);
            // add volume credits
            volumeCredits += play.Type.CalcCredits(perf);
            printData.Names.Add(play.Name);
            printData.Amounts.Add(thisAmount);
            printData.Audiences.Add(perf.Audience);
            totalAmount += thisAmount;
        }
        printData.TotalAmount = totalAmount;
        printData.VolumeCredits = volumeCredits;
       
        switch(format)
        {
            case ".txt":
                return PrintTxt(printData);
            case ".xml":
                return PrintXml(printData);
            default:
                throw new Exception("unknown format: " + format);
        }
    }

    public string PrintTxt(PrintData data){
        var result = string.Format("Statement for {0}\n", data.Customer);
        for(int i = 0; i < data.Names.Count; i++){
             // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", data.Names[i], Convert.ToDecimal(data.Amounts[i] / 100f), data.Audiences[i]);
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(data.TotalAmount / 100f));
        result += String.Format("You earned {0} credits\n", data.VolumeCredits);

        return result;
    }

    public string PrintXml(PrintData data)
    {
        //Declare a new XMLDocument object
        XmlDocument result = new XmlDocument();
        XmlDeclaration xmlDeclaration = result.CreateXmlDeclaration("1.0", "utf-8", null);
        result.AppendChild(xmlDeclaration);

        //root element
        XmlElement root = result.CreateElement("Statement");
        root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        root.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        result.AppendChild(root);

        //Customer node
        XmlElement customerNode = result.CreateElement("Customer");
        root.AppendChild(customerNode);
        XmlText customerText = result.CreateTextNode(data.Customer);
        customerNode.AppendChild(customerText);

        return result.OuterXml;
    }
}
