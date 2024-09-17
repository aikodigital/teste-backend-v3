using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Factories;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.OutputStrategies;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly IStatementOutputStrategy _outputStrategy;

    public StatementPrinter(IStatementOutputStrategy outputStrategy)
    {
        _outputStrategy = outputStrategy;
    }
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        

        foreach (var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            var strategy = PlayTypeStrategyFactory.GetStrategy(play.Type);
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = strategy.CalculateAmount(lines, perf.Audience);
            volumeCredits += strategy.CalculateVolumeCredits(perf.Audience);
           

            // print line for this order
            totalAmount += thisAmount;
        }

        return _outputStrategy.Format(invoice, plays, totalAmount, volumeCredits);
    }
}
