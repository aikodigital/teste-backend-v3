using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.Strategies;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public class StatementPrinterService: IStatementPrinterService
{
    private readonly IFormatterAdapter _formatterAdapter;
    private readonly CalculationStrategyFactory _strategyFactory;

    public StatementPrinterService(CalculationStrategyFactory strategyFactory, IFormatterAdapter formatterAdapter)
    {
        _formatterAdapter = formatterAdapter;
        _strategyFactory = strategyFactory;
    }

    public Statement BuildStatement(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statement = new Statement
        {
            Customer = invoice.Customer,
            Items = new List<StatementItem>()
        };

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var strategy = _strategyFactory.GetStrategy(play.Type);
            var amountOwed = strategy.CalculateAmount(perf, play);
            var earnedCredits = strategy.CalculateCredits(perf, play);

            var statementItem = new StatementItem
            {
                Name = play.Name,
                AmountOwed = amountOwed / 100,
                EarnedCredits = earnedCredits,
                Seats = perf.Audience,
            };

            statement.TotalAmountOwed += amountOwed;
            statement.TotalEarnedCredits += earnedCredits;
            statement.Items.Add(statementItem);
        }

        statement.TotalAmountOwed /= 100;

        return statement;
    }


    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statement = BuildStatement(invoice, plays);
        return _formatterAdapter.Format(statement);
    }

    public string Print(Statement statement)
    {
        return _formatterAdapter.Format(statement);
    }
}