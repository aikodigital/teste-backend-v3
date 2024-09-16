using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Factories;

namespace TheatricalPlayersRefactoringKata.Services.PlayAmount;

public class PlayAmountService : IPlayAmountService
{
    public decimal GetAmount(PlayEntity play, int audience)
    {
        var performanceAmountStrategy = PlayAmountStrategyFactory.CreateStrategy(play.Type);

        var lines = play.Lines switch
        {
            < 1000 => 1000,
            > 4000 => 4000,
            _ => play.Lines
        };
        var baselineAmountUnit = lines / 10m;
        var baselineAmount = baselineAmountUnit * 100;

        return performanceAmountStrategy.CalculateAmount(
            baselineAmount: baselineAmount,
            audience: audience
        );
    }
}