using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Application;

namespace TheatricalPlayersRefactoringKata.Infrastructure;

public class StatementPrinter
{
    private readonly StatementService _statementService;
    private readonly IStatementFormatter _txtFormatter;
    private readonly IStatementFormatter _xmlFormatter;

    public StatementPrinter(StatementService statementService, IStatementFormatter txtFormatter, IStatementFormatter xmlFormatter)
    {
        _statementService = statementService;
        _txtFormatter = txtFormatter;
        _xmlFormatter = xmlFormatter;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10;
            switch (play.Type)
            {
                case "tragedy":
                    if (perf.Audience > 30)
                    {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    if (perf.Audience > 20)
                    {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                case "history":
                    // Calcula o valor como tragédia
                    var tragedyAmount = thisAmount;
                    if (perf.Audience > 30)
                    {
                        tragedyAmount += 1000 * (perf.Audience - 30);
                    }

                    // Calcula o valor como comédia
                    var comedyAmount = thisAmount;
                    comedyAmount += 300 * perf.Audience;
                    if (perf.Audience > 20)
                    {
                        comedyAmount += 10000 + 500 * (perf.Audience - 20);
                    }

                    // Valor total da peça histórica
                    thisAmount = tragedyAmount + comedyAmount;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount) / 100, perf.Audience);
            totalAmount += thisAmount;
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount) / 100);
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string PrintTxt(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statementData = _statementService.GenerateStatementData(invoice, plays);
        return _txtFormatter.Format(statementData);
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statementData = _statementService.GenerateStatementData(invoice, plays);
        return _xmlFormatter.Format(statementData);
    }
}
