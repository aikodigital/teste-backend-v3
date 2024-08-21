using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Application.Factory;

namespace TheatricalPlayersRefactoringKata.Application.StatementPrinter;

public class StatementPrinter : IStatementPrinter
{
    public string TextPrint(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        decimal volumeCredits = 0;
        
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;

            decimal thisAmount = (decimal)lines / 10;
            
            var playGenre = GenreFactory.CreatePlay(play.Name, lines, play.Type);

            thisAmount = playGenre.CalculateAmount(perf.Audience);
            volumeCredits += playGenre.CalculateCredits(perf.Audience);
            
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
            totalAmount += thisAmount; 
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string XmlPrint()
    {
        throw new NotImplementedException();
    }
}