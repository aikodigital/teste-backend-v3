using System;
using TheatricalPlayersRefactoringKata.Core.Entities;

public class TragedyPlayTypeService : IPlayTypeService
{
    public int CalculateAmount(Performance performance, Play play)
    {
        int lines = Math.Clamp(play.Lines, 1000, 4000);
        int baseAmount = lines / 10;

        if (performance.Audience > 30)
        {
            baseAmount += 1000 * (performance.Audience - 30);
        }
        return baseAmount;
    }

    public int CalculateVolumeCredits(Performance performance)
    {
        return Math.Max(performance.Audience - 30, 0);
    }
}
