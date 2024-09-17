namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface ICalculateAdditionalValuePerPlayType
    {
        decimal CalculateAdditionalValue(int audience, int audienceMinimum, decimal bonus, decimal per_audience_additional, decimal per_audience);
    }
}
