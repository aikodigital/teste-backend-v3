namespace TheatricalPlayersRefactoringKata
{
    public interface IPlayAmountCalculator
    {
        int CalculateAmount(Performance perf, int baseAmount);
        int CalculateEarnedCredits(int audience);
    }
}
