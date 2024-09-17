namespace TheatricalPlayersRefactoringKata.Strategies
{
    public interface IPlayTypeStrategy
    {
        decimal CalculateAmount(int lines, int audience);
        int CalculateVolumeCredits(int audience);
    }
}
