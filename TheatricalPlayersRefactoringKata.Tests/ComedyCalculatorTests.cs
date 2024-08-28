using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class ComedyCalculatorTests
{
    [Fact]
    public void CalculateAmount_ShouldReturnCorrectAmount_WhenAudienceIsLessThanOrEqualTo20()
    {
        // Arrange
        var calculator = new ComedyCalculator();
        var play = new Play("test", 2000, "comedy");
        var performance = new Performance("test", 20);

        // Act
        var amount = calculator.CalculateAmount(performance, play);

        // Assert
        Assert.Equal(26000, amount);
    }

    [Fact]
    public void CalculateAmount_ShouldReturnCorrectAmount_WhenAudienceIsGreaterThan20()
    {
        // Arrange
        var calculator = new ComedyCalculator();
        var play = new Play("test", 2000, "comedy");
        var performance = new Performance("test", 30);

        // Act
        var amount = calculator.CalculateAmount(performance, play);

        // Assert
        Assert.Equal(44000, amount);
    }

    [Fact]
    public void CalculateVolumeCredits_ShouldReturnCorrectCredits()
    {
        // Arrange
        var calculator = new ComedyCalculator();
        var performance = new Performance("test", 40);

        // Act
        var credits = calculator.CalculateVolumeCredits(performance, null);

        // Assert
        Assert.Equal(18, credits);
    }
}

