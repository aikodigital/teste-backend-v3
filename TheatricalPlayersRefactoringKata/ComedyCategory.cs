using System;
using TheatricalPlayersRefactoringKata;

public class ComedyCategory : IPlayCategory
{
    public decimal CalculateAmount(int audience, int lines)
    {
        decimal baseAmount = Math.Max(1000, Math.Min(4000, lines)) / 10;
        decimal thisAmount = baseAmount + 3 * audience;
        if (audience > 20)
        {
            thisAmount += 100 + 5 * (audience - 20);
        }
        return thisAmount;
    }

    public int CalculateCredits(int audience)
    {
        return Math.Max(audience - 30, 0) + (int)Math.Floor((decimal)audience / 5);
    }
}
