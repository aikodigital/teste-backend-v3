namespace TheatricalPlayersRefactoringKata.Strategies.PlayAmount;

public class TragedyAmountStrategy : IPlayAmountStrategy
{
    public decimal CalculateAmount(decimal baselineAmount, int audience)
    {
        var amount = baselineAmount;
        
        if (audience > 30)
        {
            amount += 1000 * (audience - 30);
        }

        return amount;
    }
}