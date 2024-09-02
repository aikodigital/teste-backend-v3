using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Application.Strategies.Calculators;

/// <summary>
/// Calculator strategy for tragedy plays.
/// </summary>
public class TragedyCalculatorStrategy : IPlayCalculatorStrategy
{
    /// <summary>
    /// Calculates the amount for a tragedy performance.
    /// </summary>
    /// <param name="performance">The performance to calculate the amount for.</param>
    /// <returns>The calculated amount.</returns>
    public decimal CalculateAmount(Performance performance)
    {
        // Validate lines
        performance.Play.ValidateLines();

        decimal amount = performance.Play.Lines / 10;

        if (performance.Audience > 30)
        {
            amount += 10 * (performance.Audience - 30);
        }

        return amount;
    }

    /// <summary>
    /// Calculates the credits for a tragedy performance.
    /// </summary>
    /// <param name="performance">The performance to calculate the credits for.</param>
    /// <returns>The calculated credits.</returns>
    public int CalculateCredits(Performance performance)
    {
        return performance.Audience > 30 ? performance.Audience - 30 : 0;
    }
}