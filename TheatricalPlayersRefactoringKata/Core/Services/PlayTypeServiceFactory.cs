using System;
using TheatricalPlayersRefactoringKata.Core.Services.PlayTypeServices;

namespace TheatricalPlayersRefactoringKata.Core.Services;

public static class PlayTypeServiceFactory
{
    public static IPlayTypeService GetService(string playType)
    {
        return playType.ToLower() switch
        {
            "tragedy" => new TragedyPlayTypeService(),
            "comedy" => new ComedyPlayTypeService(),
            "historical" => new HistoricalPlayTypeService(),
            _ => throw new ArgumentException($"Unknown play type: {playType}")
        };
    }
}

