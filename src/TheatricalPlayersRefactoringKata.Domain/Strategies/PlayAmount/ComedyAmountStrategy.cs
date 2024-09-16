namespace TheatricalPlayersRefactoringKata.Strategies.PlayAmount;

public class ComedyAmountStrategy : IPlayAmountStrategy
{
    public decimal CalculateAmount(decimal baselineAmount, int audience)
    {
        var amount = baselineAmount + 300 * audience;

        if (audience > 20)
        {
            amount += 10000 + 500 * (audience - 20);
        }

        return amount;
    }
}