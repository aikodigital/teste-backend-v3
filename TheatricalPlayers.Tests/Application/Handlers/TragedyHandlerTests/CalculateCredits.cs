using FluentAssertions;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Interfaces.Strategies;
using Xunit;

namespace TheatricalPlayers.Tests.Application.Handlers.TragedyHandlerTests;

public class CalculateCredits
{
    private readonly PlayTypeHandler _tragedyHandler;
    
    public CalculateCredits()
    {
        _tragedyHandler = new TragedyHandler();
    }
    
    [Fact]
    public void CalculatesCredits_AudienceLessThan30()
    {
        int audience = 10, expectedCredits = 0;
        
        var credits = _tragedyHandler.CalculateCredits(audience);

        credits.Should().Be(expectedCredits);
    }
    
    [Theory]
    [InlineData(45, 15)]
    [InlineData(35, 5)]
    public void CalculatesCredits_AudienceMoreThan30(int audience, int expectedCredits)
    {
        var credits = _tragedyHandler.CalculateCredits(audience);

        credits.Should().Be(expectedCredits);
    }
}