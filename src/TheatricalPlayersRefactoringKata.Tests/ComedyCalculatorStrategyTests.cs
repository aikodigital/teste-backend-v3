using TheatricalPlayersRefactoringKata.Application.Strategies.Calculators;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Tests;

/// <summary>
/// Unit tests for the ComedyCalculatorStrategy class.
/// </summary>
public class ComedyCalculatorStrategyTests
{
    /// <summary>
    /// Tests that CalculateAmount returns the correct amount for a given performance.
    /// </summary>
    [Fact]
    public void CalculateAmount_ShouldReturnCorrectAmount()
    {
        // Arrange
        var play = new Play("Henry V", GenreEnum.Comedy, 1200);
        var performance = new Performance(play, 25);
        var strategy = new ComedyCalculatorStrategy();

        // Act
        var amount = strategy.CalculateAmount(performance);

        // Assert
        Assert.Equal(320m, amount);
    }

    /// <summary>
    /// Tests that CalculateCredits returns the correct credits for a given performance.
    /// </summary>
    [Fact]
    public void CalculateCredits_ShouldReturnCorrectCredits()
    {
        // Arrange
        var play = new Play("Henry V", GenreEnum.Comedy, 1200);
        var performance = new Performance(play, 35);
        var strategy = new ComedyCalculatorStrategy();

        // Act
        var credits = strategy.CalculateCredits(performance);

        // Assert
        Assert.Equal(12, credits);
    }
}