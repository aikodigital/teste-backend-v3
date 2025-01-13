namespace TheatricalPlayersRefactoringKata.Application.Services.CalculatorGenre
{
    public class CalculatorGenreComedy : ICalculatorGenre
    {
        public (decimal amount, int credit) CalculatePerformanceOfValues(int lines, int audience)
        {
            // sets default
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            // sets default
            var amount = lines * 10;

            // sets rules especific to amount
            if (audience > 20)
            {
                amount += 10000 + 500 * (audience - 20);
            }
            amount += 300 * audience;

            // sets default
            var credit = Math.Max(audience - 30, 0);

            // add extra credit for every ten comedy attendees
            credit += (int)Math.Floor((decimal)audience / 5);

            return (amount, credit);
        }
    }
}
