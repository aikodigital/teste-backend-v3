using System;


public class ComedyCategory : IPlayCategory
{
    public decimal CalculateAmount(int audience, int lines)
    {
        decimal baseAmount = Math.Max(100.0m, Math.Min(400.0m, lines / 10.0m));

        decimal additionalAmount = 3.0m * audience;

        if (audience > 20)
        {
            additionalAmount += 100.0m + 5.0m * (audience - 20);
        }

        return baseAmount + additionalAmount;
    }

    public int CalculateCredits(int audience)
    {
        int baseCredits = audience > 30 ? audience - 30 : 0;
        int bonusCredits = audience / 5;

        return baseCredits + bonusCredits;
    }
}
