using System;
using TheatricalPlayersRefactoringKata.Enum;
using TheatricalPlayersRefactoringKata.Strategies.PlayAmount;

namespace TheatricalPlayersRefactoringKata.Factories;

public static class PlayAmountStrategyFactory
{
    public static IPlayAmountStrategy CreateStrategy(PlayTypeEnum playType)
    {
        return playType switch
        {
            PlayTypeEnum.Tragedy => new TragedyAmountStrategy(),
            PlayTypeEnum.Comedy => new ComedyAmountStrategy(),
            PlayTypeEnum.History => new HistoryAmountStrategy(),
            _ => throw new Exception("unknown type: " + playType)
        };
    }
}