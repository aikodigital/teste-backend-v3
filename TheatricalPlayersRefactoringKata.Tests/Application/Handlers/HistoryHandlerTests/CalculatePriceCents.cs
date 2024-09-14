using FluentAssertions;
using Moq;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Interfaces.Strategies;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Application.Handlers.HistoryHandlerTests;

public class CalculatePriceCents
{
    private readonly PlayTypeHandler _historicalHandler;
    private readonly Mock<ComedyHandler> _mockComedyHandler;
    private readonly Mock<TragedyHandler> _mockTragedyHandler;
    
    public CalculatePriceCents()
    {
        _mockComedyHandler = new();
        _mockTragedyHandler = new();
        _historicalHandler = new HistoryHandler(_mockComedyHandler.Object, _mockTragedyHandler.Object);
    }

    [Fact]
    public void CalculatePriceCentsOk()
    {
        int lines = 10, audience = 100, expectedPriceCents = 12000;

        _mockComedyHandler.Setup(m => m.CalculatePriceCents(lines, audience))
            .Returns(10000);
        
        _mockTragedyHandler.Setup(m => m.CalculatePriceCents(lines, audience))
            .Returns(2000);
        
        var priceCents = _historicalHandler.CalculatePriceCents(lines, audience);

        priceCents.Should().Be(expectedPriceCents);
    }
    
}