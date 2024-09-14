using FluentAssertions;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Interfaces.Strategies;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Application.Handlers.TragedyHandlerTests;

public class CalculatePriceCents
{
    private readonly PlayTypeHandler _tragedyHandler;
    
    public CalculatePriceCents()
    {
        _tragedyHandler = new TragedyHandler();
    }

    [Fact]
    public void CalculatePrice_AudienceLessThan30()
    {
        int lines = 10, audience = 10, expectedPriceCents = 10000;
        
        var priceCents = _tragedyHandler.CalculatePriceCents(lines, audience);

        priceCents.Should().Be(expectedPriceCents);
    }

    [Fact]
    public void CalculatePrice_AudienceGreaterThan30()
    {
        int lines = 10, audience = 35, expectedPriceCents = 15000;
        
        var priceCents = _tragedyHandler.CalculatePriceCents(lines, audience);

        priceCents.Should().Be(expectedPriceCents);
    }
}