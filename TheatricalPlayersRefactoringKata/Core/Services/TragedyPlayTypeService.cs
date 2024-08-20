using System;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Services.PlayTypeServices;

public class TragedyPlayTypeService : IPlayTypeService
{
    public int CalculateAmount(Performance performance, Play play)
    {
        var lines = play.Lines < 1000 ? 1000 : play.Lines > 4000 ? 4000 : play.Lines;
        var thisAmount = lines * 10;
        if (performance.Audience > 30)
        {
            thisAmount += 1000 * (performance.Audience - 30);
        }
        return thisAmount;
    }

    public int CalculateVolumeCredits(Performance performance)
    {
        return Math.Max(performance.Audience - 30, 0);
    }
}

