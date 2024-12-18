using System;

namespace TheatricalPlayersRefactoringKata.Services.PlayType;

public class ComedyPlayCalculator : IPlayCalculator
{
    public decimal CalculateAmount(Play play, int audience)
    {
        int lines = Math.Clamp(play.Lines, 1000, 4000);
        decimal baseAmount = lines / 10m;

        baseAmount += 3 * audience;

        if (audience > 20)
        {
            baseAmount += 100 + 5 * (audience - 20);
        }

        return Math.Round(baseAmount, 2);
    }

    public int CalculateCredits(Play play, int audience)
    {
        if (audience <= 30)
            return 0;

        int baseCredits = audience - 30;
        int bonusCredits = (int)Math.Floor(audience / 5.0); 
        return baseCredits + bonusCredits;
    }
}

