using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinterService
{
    public async Task<string> Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var result = GenerateTextStatement(invoice, plays);
        return result;
    }

    public async Task<string> PrintAsXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var result = GenerateXmlStatement(invoice, plays);
        return result;
    }

    private string GenerateTextStatement(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = CalculateAmount(play, perf);

            // add volume credits
            volumeCredits += CalculateVolumeCredits(play, perf);

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }

    private string GenerateXmlStatement(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = "<Statement>\n";
        result += $"  <Customer>{invoice.Customer}</Customer>\n";
        result += "  <Items>\n";

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = CalculateAmount(play, perf);

            // add volume credits
            volumeCredits += CalculateVolumeCredits(play, perf);

            // print line for this order
            result += "    <Item>\n";
            result += $"      <Play>{play.Name}</Play>\n";
            result += $"      <AmountOwed>{Convert.ToDecimal(thisAmount / 100)}</AmountOwed>\n";
            result += $"      <Seats>{perf.Audience}</Seats>\n";
            result += $"      <EarnedCredits>{volumeCredits}</EarnedCredits>\n";
            result += "    </Item>\n";
            totalAmount += thisAmount;
        }

        result += "  </Items>\n";
        result += $"  <TotalAmount>{Convert.ToDecimal(totalAmount / 100)}</TotalAmount>\n";
        result += $"  <TotalCredits>{volumeCredits}</TotalCredits>\n";
        result += "</Statement>\n";
        return result;
    }

    private int CalculateAmount(Play play, Performance perf)
    {
        var lines = play.Lines;
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        var thisAmount = lines * 10;

        switch (play.Type)
        {
            case "tragedy":
                if (perf.Audience > 30)
                    thisAmount += 1000 * (perf.Audience - 30);
                break;

            case "comedy":
                if (perf.Audience > 20)
                    thisAmount += 10000 + 500 * (perf.Audience - 20);
                thisAmount += 300 * perf.Audience;
                break;

            case "history":
                if (perf.Audience > 40)
                    thisAmount += 1200 * (perf.Audience - 40);
                break;

            default:
                throw new Exception("unknown type: " + play.Type);
        }

        return thisAmount;
    }

    private int CalculateVolumeCredits(Play play, Performance perf)
    {
        var volumeCredits = Math.Max(perf.Audience - 30, 0);

        if ("comedy" == play.Type)
            volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

        return volumeCredits;
    }
}

