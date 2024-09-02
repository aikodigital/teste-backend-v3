using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Application.Strategies.Calculators;

/// <summary>
/// Interface for play calculator strategies.
/// </summary>
public interface IPlayCalculatorStrategy
{
    /// <summary>
    /// Calculates the amount for a given performance.
    /// </summary>
    /// <param name="performance">The performance to calculate the amount for.</param>
    /// <returns>The calculated amount.</returns>
    decimal CalculateAmount(Performance performance);

    /// <summary>
    /// Calculates the credits for a given performance.
    /// </summary>
    /// <param name="performance">The performance to calculate the credits for.</param>
    /// <returns>The calculated credits.</returns>
    int CalculateCredits(Performance performance);
}