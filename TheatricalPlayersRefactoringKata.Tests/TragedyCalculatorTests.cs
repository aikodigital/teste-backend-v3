using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class TragedyCalculatorTests
{
    [Fact]
    public void CalculateAmount_ShouldReturnCorrectAmount_WhenAudienceIsLessThanOrEqualTo30()
    {
        // Arrange
        var calculator = new TragedyCalculator();
        var play = new Play("test", 1500, "tragedy");
        var performance = new Performance("test", 30);

        // Act
        var amount = calculator.CalculateAmount(performance, play);

        // Assert
        Assert.Equal(15000, amount);
    }

    [Fact]
    public void CalculateAmount_ShouldReturnCorrectAmount_WhenAudienceIsGreaterThan30()
    {
        // Arrange
        var calculator = new TragedyCalculator();
        var play = new Play("test", 1500, "tragedy");
        var performance = new Performance("test", 40);

        // Act
        var amount = calculator.CalculateAmount(performance, play);

        // Assert
        Assert.Equal(25000, amount);
    }

    [Fact]
    public void CalculateVolumeCredits_ShouldReturnCorrectCredits()
    {
        // Arrange
        var calculator = new TragedyCalculator();
        var performance = new Performance("test", 35);

        // Act
        var credits = calculator.CalculateVolumeCredits(performance, null);

        // Assert
        Assert.Equal(5, credits);
    }
}

