namespace TheatricalPlayersRefactoringKata.Application.Services.CalculatorGenre
{
    public class CalculatorGenreContext
    {
        private ICalculatorGenre _genre;
        private int _lines;
        private int _audience;

        public CalculatorGenreContext(ICalculatorGenre genre, int lines, int audience)
        {
            _genre = genre;
            _lines = lines;
            _audience = audience;
        }

        public (decimal amount, int credit) CalculatePerformanceOfValues()
        {
            return _genre.CalculatePerformanceOfValues(_lines, _audience);
        }
    }
}
