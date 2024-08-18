using System;
using TheatricalPlayersRefactoringKata;

public class TragedyCategory : IPlayCategory
{
    public decimal CalculateAmount(int audience, int lines)
    {
        decimal baseAmount = Math.Max(1000, Math.Min(4000, lines)) / 10;
        if (audience > 30)
        {
            baseAmount += 10 * (audience - 30);
        }
        return baseAmount;
    }

    public int CalculateCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }
}
