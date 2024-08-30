using System;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Application.Strategies;

public class HistoryCalculationStrategy : ICalculationStrategy
{
    private readonly CalculationStrategyFactory _strategyFactory;

    public HistoryCalculationStrategy(CalculationStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    public decimal CalculateAmount(Performance perf, Play play)
    {
        // TODO: FAZ SENTIDO???
        var tragedyStrategy = _strategyFactory.GetStrategy("tragedy");
        var comedyStrategy = _strategyFactory.GetStrategy("comedy");

        var tragedyAmount = tragedyStrategy.CalculateAmount(perf, play);
        var comedyAmount = comedyStrategy.CalculateAmount(perf, play);

        return tragedyAmount + comedyAmount;
    }

    public decimal CalculateCredits(Performance perf, Play play) => Math.Max(perf.Audience - 30, 0);
}
