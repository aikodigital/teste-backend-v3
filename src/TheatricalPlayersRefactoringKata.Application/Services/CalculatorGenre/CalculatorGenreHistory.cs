namespace TheatricalPlayersRefactoringKata.Application.Services.CalculatorGenre
{
    public class CalculatorGenreHistory : ICalculatorGenre
    {
        public (decimal amount, int credit) CalculatePerformanceOfValues(int lines, int audience)
        {
            var comedyAmount = new CalculatorGenreComedy().CalculatePerformanceOfValues(lines, audience).amount;
            var tragedyAmount = new CalculatorGenreTragedy().CalculatePerformanceOfValues(lines, audience).amount;

            // sets the amount with the sum of values of the genres comedy and tragedy
            var amount = comedyAmount + tragedyAmount;

            // sets default
            var credit = Math.Max(audience - 30, 0);

            return (amount, credit);
        }
    }
}
