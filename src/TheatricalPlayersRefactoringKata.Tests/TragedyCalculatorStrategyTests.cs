using TheatricalPlayersRefactoringKata.Application.Strategies.Calculators;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Tests;

/// <summary>
/// Unit tests for the TragedyCalculatorStrategy class.
/// </summary>
public class TragedyCalculatorStrategyTests
{
    /// <summary>
    /// Tests that CalculateAmount returns the correct amount for a given performance.
    /// </summary>
    [Fact]
    public void CalculateAmount_ShouldReturnCorrectAmount()
    {
        // Arrange
        var play = new Play("Henry V", GenreEnum.Tragedy, 2000);
        var performance = new Performance(play, 50);
        var strategy = new TragedyCalculatorStrategy();

        // Act
        var amount = strategy.CalculateAmount(performance);

        // Assert
        Assert.Equal(400m, amount);
    }

    /// <summary>
    /// Tests that CalculateCredits returns the correct credits for a given performance.
    /// </summary>
    [Fact]
    public void CalculateCredits_ShouldReturnCorrectCredits()
    {
        // Arrange
        var play = new Play("Henry V", GenreEnum.Tragedy, 2000);
        var performance = new Performance(play, 35);
        var strategy = new TragedyCalculatorStrategy();

        // Act
        var credits = strategy.CalculateCredits(performance);

        // Assert
        Assert.Equal(5, credits);
    }
}