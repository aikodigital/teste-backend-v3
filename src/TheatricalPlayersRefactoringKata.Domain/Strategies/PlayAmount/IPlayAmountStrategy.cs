namespace TheatricalPlayersRefactoringKata.Strategies.PlayAmount;

public interface IPlayAmountStrategy
{
    decimal CalculateAmount(decimal baselineAmount, int audience);
}