using System.Globalization;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Enum;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services;

public class StatementPrinterService : IStatementPrinterService
{
    public string Print(InvoiceEntity invoice, Dictionary<string, PlayEntity> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";
        var cultureInfo = new CultureInfo("en-US");

        foreach(var performance in invoice.Performances) 
        {
            var play = plays[performance.PlayId];
            
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10;
            
            switch (play.Type) 
            {
                case PlayTypeEnum.Tragedy:
                    if (performance.Audience > 30) {
                        thisAmount += 1000 * (performance.Audience - 30);
                    }
                    break;
                case PlayTypeEnum.Comedy:
                    if (performance.Audience > 20) {
                        thisAmount += 10000 + 500 * (performance.Audience - 20);
                    }
                    thisAmount += 300 * performance.Audience;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            
            // add volume credits
            volumeCredits += Math.Max(performance.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if (play.Type == PlayTypeEnum.Comedy) volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), performance.Audience);
            totalAmount += thisAmount;
        }
        
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += $"You earned {volumeCredits} credits\n";
        
        return result;
    }
}
