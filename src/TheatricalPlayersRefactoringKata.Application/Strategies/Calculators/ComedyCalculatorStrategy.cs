using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Application.Strategies.Calculators
{
    /// <summary>
    /// Calculator strategy for comedy plays.
    /// </summary>
    public class ComedyCalculatorStrategy : IPlayCalculatorStrategy
    {
        /// <summary>
        /// Calculates the amount for a comedy performance.
        /// </summary>
        /// <param name="performance">The performance to calculate the amount for.</param>
        /// <returns>The calculated amount.</returns>
        public decimal CalculateAmount(Performance performance)
        {
            // Validate lines
            performance.Play.ValidateLines();

            decimal amount = performance.Play.Lines / 10;
            amount += 3 * performance.Audience;

            if (performance.Audience > 20)
            {
                amount += 100 + 5 * (performance.Audience - 20);
            }

            return amount;
        }

        /// <summary>
        /// Calculates the credits for a comedy performance.
        /// </summary>
        /// <param name="performance">The performance to calculate the credits for.</param>
        /// <returns>The calculated credits.</returns>
        public int CalculateCredits(Performance performance)
        {
            var credits = performance.Audience > 30 ? performance.Audience - 30 : 0;
            credits += performance.Audience / 5;
            return credits;
        }
    }
}
