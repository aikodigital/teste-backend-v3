using System;


public class TragedyCategory : IPlayCategory
{
    public decimal CalculateAmount(int audience, int lines)
    {
        decimal baseAmount = Math.Max(100.0m, Math.Min(400.0m, lines / 10.0m));

        decimal additionalAmount = (audience > 30) ? 10.0m * (audience - 30) : 0.0m;

        return baseAmount + additionalAmount;
    }

    public int CalculateCredits(int audience)
    {
        return (audience > 30) ? (audience - 30) : 0;
    }
}
