using System;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Services.PlayTypeServices;

public class ComedyPlayTypeService : IPlayTypeService
{
    public int CalculateAmount(Performance performance, Play play)
    {
        var lines = play.Lines < 1000 ? 1000 : play.Lines > 4000 ? 4000 : play.Lines;
        var thisAmount = lines * 10;

        thisAmount += 300 * performance.Audience;

        if (performance.Audience > 20)
        {
                thisAmount += 10000 + 500 * (performance.Audience - 20);
        }

        return thisAmount;
    }

    public int CalculateVolumeCredits(Performance performance)
    {
        var volumeCredits = Math.Max(performance.Audience - 30, 0);
        volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);
        return volumeCredits;
    }
}

