namespace TheatricalPlayersRefactoringKata.Interfaces
{
    public interface IStatementStrategy
    {
        decimal CalculatePrice(Play play, Performance perf);
        int CalculateCredits(Play play, Performance perf);
    }
}
