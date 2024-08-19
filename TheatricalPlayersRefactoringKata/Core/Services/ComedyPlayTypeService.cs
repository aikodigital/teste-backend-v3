using System;
using TheatricalPlayersRefactoringKata.Core.Entities;

public class ComedyPlayTypeService : IPlayTypeService
{
    public int CalculateAmount(Performance performance, Play play)
    {
        int lines = Math.Clamp(play.Lines, 1000, 4000);
        int baseAmount = lines / 10;
        baseAmount += 300 * performance.Audience;

        if (performance.Audience > 20)
        {
            baseAmount += 10000 + 500 * (performance.Audience - 20);
        }
        return baseAmount;
    }

    public int CalculateVolumeCredits(Performance performance)
    {
        int credits = Math.Max(performance.Audience - 30, 0);
        credits += (int)Math.Floor(performance.Audience / 5m);
        return credits;
    }
}
