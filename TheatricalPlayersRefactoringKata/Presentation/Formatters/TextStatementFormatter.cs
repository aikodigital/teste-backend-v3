using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Services;

namespace TheatricalPlayersRefactoringKata.Core.Presentation.Formatters;

public class TextStatementFormatter : IStatementFormatter
{
    private readonly StatementService _statementService;

    public TextStatementFormatter(StatementService statementService)
    {
        _statementService = statementService;
    }

    public string Format(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var service = PlayTypeServiceFactory.GetService(play.Type);

            var thisAmount = service.CalculateAmount(perf, play);

            volumeCredits += service.CalculateVolumeCredits(perf);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}

