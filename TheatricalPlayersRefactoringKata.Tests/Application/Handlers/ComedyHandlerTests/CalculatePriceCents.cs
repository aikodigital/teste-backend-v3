using FluentAssertions;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Interfaces.Strategies;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Application.Handlers.ComedyHandlerTests;

public class CalculatePriceCents
{
    private readonly PlayTypeHandler _comedyHandler;
    
    public CalculatePriceCents()
    {
        _comedyHandler = new ComedyHandler();
    }

    [Fact]
    public void CalculatePrice_AudienceLessThan20()
    {
        int lines = 10, audience = 15, expectedPriceCents = 14500;
        
        var priceCents = _comedyHandler.CalculatePriceCents(lines, audience);

        priceCents.Should().Be(expectedPriceCents);
    }
    
    [Fact]
    public void CalculatePrice_AudienceGreaterThan20()
    {
        int lines = 10, audience = 55, expectedPriceCents = 54000;
        
        var price = _comedyHandler.CalculatePriceCents(lines, audience);

        price.Should().Be(expectedPriceCents);
    }
}