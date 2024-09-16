using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Enums;
using TheatricalPlayers.Core.Interfaces.Strategies;

namespace TheatricalPlayers.Application.Factories;

public static class PlayTypeFactory
{
    public static PlayTypeHandler GetHandler(PlayTypeEnum playType)
    {
        return playType switch
        {
            PlayTypeEnum.Tragedy => new TragedyHandler(),
            PlayTypeEnum.Comedy => new ComedyHandler(),
            PlayTypeEnum.Historical => new HistoryHandler(new ComedyHandler(), new TragedyHandler()),
            _ => throw new ArgumentException($"Unknown play type: {playType}", nameof(playType))
        };
    }
}