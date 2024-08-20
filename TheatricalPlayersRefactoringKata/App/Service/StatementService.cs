using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Services;

public class StatementService
{
    public string GenerateStatement(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = "";

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var service = PlayTypeServiceFactory.GetService(play.Type);
                
            var thisAmount = service.CalculateAmount(perf, play);

            volumeCredits += service.CalculateVolumeCredits(perf);

            result += $"  {play.Name}: {thisAmount / 100:C} ({perf.Audience} seats)\n";
            totalAmount += thisAmount;
        }

        result += $"Amount owed is {totalAmount / 100:C}\n";
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }
}

