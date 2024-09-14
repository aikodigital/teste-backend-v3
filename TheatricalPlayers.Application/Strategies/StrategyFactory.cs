using TheatricalPlayers.Core.Enums;
using TheatricalPlayers.Core.Interfaces.Strategies;

namespace TheatricalPlayers.Application.Strategies;

public static class StrategyFactory
{
    public static IPlayTypeStrategy GetStrategy(PlayTypeEnum playType)
    {
        return playType switch
        {
            PlayTypeEnum.Tragedy => new TragedyStrategy(),
            PlayTypeEnum.Comedy => new ComedyStrategy(),
            _ => throw new ArgumentException($"Unknown play type: {playType}", nameof(playType))
        };
    }
}