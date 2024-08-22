using System;

public class HistoricalCategory : IPlayCategory
{
    private readonly TragedyCategory _tragedyCategory;
    private readonly ComedyCategory _comedyCategory;

    public HistoricalCategory()
    {
        _tragedyCategory = new TragedyCategory();
        _comedyCategory = new ComedyCategory();
    }

    public decimal CalculateAmount(int audience, int lines)
    {
        decimal tragedyAmount = _tragedyCategory.CalculateAmount(audience, lines);
        decimal comedyAmount = _comedyCategory.CalculateAmount(audience, lines);
        return tragedyAmount + comedyAmount;
    }

    public int CalculateCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }
}
