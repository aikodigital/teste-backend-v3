namespace TheatricalPlayersRefactoringKata.Strategies
{
    public interface IPlayStrategy
    {
        double CalculateAmount(int audience, int lines);
        int CalculateCredits(int audience);
    }
}
