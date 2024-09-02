using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Application.Strategies.Calculators
{
    /// <summary>
    /// Calculator strategy for historical plays.
    /// </summary>
    public class HistoricalCalculatorStrategy : IPlayCalculatorStrategy
    {
        private readonly TragedyCalculatorStrategy _tragedyCalculator = new TragedyCalculatorStrategy();
        private readonly ComedyCalculatorStrategy _comedyCalculator = new ComedyCalculatorStrategy();

        /// <summary>
        /// Calculates the amount for a historical performance.
        /// </summary>
        /// <param name="performance">The performance to calculate the amount for.</param>
        /// <returns>The calculated amount.</returns>
        public decimal CalculateAmount(Performance performance)
        {
            // Validate lines
            performance.Play.ValidateLines();

            return _tragedyCalculator.CalculateAmount(performance) + _comedyCalculator.CalculateAmount(performance);
        }

        /// <summary>
        /// Calculates the credits for a historical performance.
        /// </summary>
        /// <param name="performance">The performance to calculate the credits for.</param>
        /// <returns>The calculated credits.</returns>
        public int CalculateCredits(Performance performance)
        {
            return _tragedyCalculator.CalculateCredits(performance) + _comedyCalculator.CalculateCredits(performance);
        }
    }
}
