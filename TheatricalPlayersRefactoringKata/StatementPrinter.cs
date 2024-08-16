using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public const string TEXT_MODE = "text";
    public const string XML_MODE = "xml";

    public const int MIN_AUDIENCE_FOR_CREDITS = 30;
    
    public string Print(Invoice invoice, Dictionary<string, Play> plays, string printMode)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        CultureInfo cultureInfo = new CultureInfo("en-US");
        
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        
        if (printMode == XML_MODE){
            result = "\ufeff<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            result += "<Statement xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n";
            result += $"  <Customer>{invoice.Customer}</Customer>\n";
            result +=  "  <Items>\n";
        }

        foreach(var perf in invoice.Performances){
            int itemCredits;
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var itemAmount = lines * 10;
            switch (play.Genre) 
            {
                case Genres.TRAGEDY:
                    itemAmount = CalculateTragedyAmount(perf, itemAmount);
                    break;
                case Genres.COMEDY:
                    itemAmount = CalculateComedyAmount(perf, itemAmount);
                    break;
                case Genres.HISTORY:
                    itemAmount = CalculateHistoryAmount(perf, itemAmount);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Genre);
            }
            // add item credits
            itemCredits = Math.Max(perf.Audience - MIN_AUDIENCE_FOR_CREDITS, 0);
            // add extra credit for every ten comedy attendees
            if (play.Genre == Genres.COMEDY) itemCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // add volume credits
            volumeCredits += itemCredits;
            // print line for this order
            switch (printMode){
                case TEXT_MODE:
                    result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, (decimal)itemAmount / 100, perf.Audience);
                    break;
                case XML_MODE:
                    result +=  "    <Item>\n";
                    result += String.Format(cultureInfo, 
                               "      <AmountOwed>{0}</AmountOwed>\n", (decimal)itemAmount / 100);
                    result += $"      <EarnedCredits>{itemCredits}</EarnedCredits>\n";
                    result += $"      <Seats>{perf.Audience}</Seats>\n";    
                    result += "    </Item>\n";
                    break;
            }
            totalAmount += itemAmount;
        }
        switch (printMode){
            case TEXT_MODE:
                result += String.Format(cultureInfo, "Amount owed is {0:C}\n", (decimal)totalAmount / 100);
                result += String.Format("You earned {0} credits\n", volumeCredits);
                break;
            case XML_MODE:
                result += "  </Items>\n";
                result += String.Format(cultureInfo, "  <AmountOwed>{0}</AmountOwed>\n", (decimal)totalAmount / 100);
                result += $"  <EarnedCredits>{volumeCredits}</EarnedCredits>\n";
                result += "</Statement>";
                break;
        }
        
        return result;
    }

    public int CalculateTragedyAmount(Performance perf, int amount){
        if(perf.Audience < 30) return amount;
        return amount + 1000 * (perf.Audience - 30);
    }
    
    
    public int CalculateComedyAmount(Performance perf, int amount){
        var newAmount = amount;
        if (perf.Audience > 20) {
            newAmount += 10000 + 500 * (perf.Audience - 20);
        }
        return newAmount + 300 * perf.Audience;
    }

    public int CalculateHistoryAmount(Performance perf, int amount){
        return CalculateTragedyAmount(perf, amount) + CalculateComedyAmount(perf, amount);
    }
}
