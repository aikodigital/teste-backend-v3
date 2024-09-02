using TheatricalPlayersRefactoringKata.Application.Strategies.Calculators;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Tests;

/// <summary>
/// Unit tests for the HistoricalCalculatorStrategy class.
/// </summary>
public class HistoricalCalculatorStrategyTests
{
    /// <summary>
    /// Tests that CalculateAmount returns the correct amount for a given performance.
    /// </summary>
    [Fact]
    public void CalculateAmount_ShouldReturnCorrectAmount()
    {
        // Arrange
        var play = new Play("Henry V", GenreEnum.Historical, 1200);
        var performance = new Performance(play, 25);
        var strategy = new HistoricalCalculatorStrategy();

        // Act
        var amount = strategy.CalculateAmount(performance);

        // Calculation: Comedy amount + Tragedy amount
        var expectedAmount = new ComedyCalculatorStrategy().CalculateAmount(performance) +
                             new TragedyCalculatorStrategy().CalculateAmount(performance);

        // Assert
        Assert.Equal(expectedAmount, amount);
    }

    /// <summary>
    /// Tests that CalculateCredits returns the correct credits for a given performance.
    /// </summary>
    [Fact]
    public void CalculateCredits_ShouldReturnCorrectCredits()
    {
        // Arrange
        var play = new Play("Henry V", GenreEnum.Historical, 1200);
        var performance = new Performance(play, 35);
        var strategy = new HistoricalCalculatorStrategy();

        // Act
        var credits = strategy.CalculateCredits(performance);

        // Calculation: Comedy credits + Tragedy credits
        var expectedCredits = new ComedyCalculatorStrategy().CalculateCredits(performance) +
                              new TragedyCalculatorStrategy().CalculateCredits(performance);

        // Assert
        Assert.Equal(expectedCredits, credits);
    }
}