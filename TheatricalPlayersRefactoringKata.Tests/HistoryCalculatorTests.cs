using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class HistoryCalculatorTests
{
    [Fact]
    public void CalculateAmount_ShouldReturnCorrectAmount_ForMixedAudience()
    {
        // Arrange
        var calculator = new HistoryCalculator();
        var play = new Play("test", 3000, "history");
        var performance = new Performance("test", 39);

        // Act
        var amount = calculator.CalculateAmount(performance, play);

        // Assert
        Assert.Equal(100200, amount);
    }

    [Fact]
    public void CalculateVolumeCredits_ShouldReturnCorrectCredits()
    {
        // Arrange
        var calculator = new HistoryCalculator();
        var performance = new Performance ("test", 39);

        // Act
        var credits = calculator.CalculateVolumeCredits(performance, null);

        // Assert
        Assert.Equal(9, credits);
    }

    [Fact]
    public void CalculateAmount_ShouldReturnCorrectAmount_ForLowerAudience()
    {
        // Arrange
        var calculator = new HistoryCalculator();
        var play = new Play("test", 2000, "history");
        var performance = new Performance("test", 25);

        // Act
        var amount = calculator.CalculateAmount(performance, play);

        // Assert
        Assert.Equal(60000, amount);
    }

    [Fact]
    public void CalculateVolumeCredits_ShouldReturnCorrectCredits_ForLowerAudience()
    {
        // Arrange
        var calculator = new HistoryCalculator();
        var performance = new Performance("test", 25);

        // Act
        var credits = calculator.CalculateVolumeCredits(performance, null);

        // Assert
        Assert.Equal(0, credits);
    }
}

