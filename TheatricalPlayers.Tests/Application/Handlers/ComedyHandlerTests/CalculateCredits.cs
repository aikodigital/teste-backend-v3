using FluentAssertions;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Interfaces.Strategies;
using Xunit;

namespace TheatricalPlayers.Tests.Application.Handlers.ComedyHandlerTests;

public class CalculateCredits
{
    private readonly PlayTypeHandler _comedyHandler;
    
    public CalculateCredits()
    {
        _comedyHandler = new ComedyHandler();
    }

    [Fact]
    public void CalculatesCreditsWithBonus()
    {
        int audience = 100000, expectedCredits = 119970;
        
        var credits = _comedyHandler.CalculateCredits(audience);
        
        credits.Should().Be(expectedCredits);
    }
    
}