using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Tests;

public class PlayTests
{
    /// <summary>
    /// Verifies that ValidateLines throws an ArgumentOutOfRangeException
    /// when the number of lines is less than 1000.
    /// </summary>
    [Fact]
    public void ValidateLines_ShouldThrowArgumentOutOfRangeException_WhenLinesAreLessThan1000()
    {
        // Arrange
        var play = new Play("As You Like It", GenreEnum.Comedy, 999);

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => play.ValidateLines());
        Assert.Contains("Lines must be between 1000 and 4000", exception.Message);
    }

    /// <summary>
    /// Verifies that ValidateLines throws an ArgumentOutOfRangeException
    /// when the number of lines is more than 4000.
    /// </summary>
    [Fact]
    public void ValidateLines_ShouldThrowArgumentOutOfRangeException_WhenLinesAreMoreThan4000()
    {
        // Arrange
        var play = new Play("As You Like It", GenreEnum.Comedy, 4001);

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => play.ValidateLines());
        Assert.Contains("Lines must be between 1000 and 4000", exception.Message);
    }

    /// <summary>
    /// Verifies that ValidateLines does not throw any exception
    /// when the number of lines is within the valid range (1000 to 4000).
    /// </summary>
    [Fact]
    public void ValidateLines_ShouldNotThrowException_WhenLinesAreWithinRange()
    {
        // Arrange
        var play = new Play("As You Like It", GenreEnum.Comedy, 2000);

        // Act & Assert
        var exception = Record.Exception(() => play.ValidateLines());
        Assert.Null(exception);
    }
}