using System;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services.PlayType;

public class TragedyPlayCalculator : IPlayCalculator
{
    public decimal CalculateAmount(Play play, int audience)
    {
        int lines = Math.Clamp(play.Lines, 1000, 4000);
        decimal baseAmount = lines / 10m;

        if (audience > 30)
        {
            baseAmount += 10 * (audience - 30);
        }

        return Math.Round(baseAmount, 2);
    }

    public int CalculateCredits(Play play, int audience)
    {
        if (audience <= 30)
        {
            return 0;
        }

        return audience - 30;
    }

}
