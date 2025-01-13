namespace TheatricalPlayersRefactoringKata.Application.Services.CalculatorGenre
{
    public class CalculatorGenreTragedy : ICalculatorGenre
    {
        public (decimal amount, int credit) CalculatePerformanceOfValues(int lines, int audience)
        {
            // sets default
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            // sets default
            var amount = lines * 10;

            // sets rules especific to amount
            if (audience > 30)
            {
                amount += 1000 * (audience - 30);
            }

            // sets default
            var credit = Math.Max(audience - 30, 0);

            return (amount, credit);
        }
    }
}
