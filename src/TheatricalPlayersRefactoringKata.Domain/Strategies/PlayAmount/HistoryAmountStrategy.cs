namespace TheatricalPlayersRefactoringKata.Strategies.PlayAmount;

public class HistoryAmountStrategy : IPlayAmountStrategy
{
    public decimal CalculateAmount(decimal baselineAmount, int audience)
    {
        var tragedyAmount = new TragedyAmountStrategy().CalculateAmount(baselineAmount, audience);
        var comedyAmount = new ComedyAmountStrategy().CalculateAmount(baselineAmount, audience);
        
        return tragedyAmount + comedyAmount;
    }
}