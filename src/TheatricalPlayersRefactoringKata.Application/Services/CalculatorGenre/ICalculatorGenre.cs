namespace TheatricalPlayersRefactoringKata.Application.Services.CalculatorGenre
{
    public interface ICalculatorGenre
    {
        public (decimal amount, int credit) CalculatePerformanceOfValues(int lines, int audience);
    }
}
