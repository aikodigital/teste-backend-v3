using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services.PlayType;
using TheatricalPlayersRefactoringKata.Services.Formatters;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    public Statement GenerateStatement(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statement = new Statement { Customer = invoice.Customer };

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var calculator = PlayCalculatorFactory.Create(play.Type);

            var item = new StatementItem
            {
                PlayName = play.Name,
                AmountOwed = calculator.CalculateAmount(play, perf.Audience),
                EarnedCredits = calculator.CalculateCredits(play, perf.Audience),
                Seats = perf.Audience
            };

            statement.Items.Add(item);
            statement.TotalAmount += item.AmountOwed;
            statement.TotalCredits += item.EarnedCredits;
        }
        statement.TotalAmount = Math.Round(statement.TotalAmount, 2);

        return statement;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statement = GenerateStatement(invoice, plays);
        return GenerateTextStatement(statement, plays);
    }

    public string GenerateTextStatement(Statement statement, Dictionary<string, Play> plays)
    {
        var formatter = new TextStatementFormatter();
        return formatter.Format(statement);
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statement = GenerateStatement(invoice, plays);
        var formatter = new XmlStatementFormatter();
        return formatter.Format(statement);
    }
}
