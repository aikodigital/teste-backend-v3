using TheatricalPlayersRefactoringKata.Models;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class PerformanceTests
{
    [Fact]
    public void CriacaoValida()
    {
        var playId = "Dom Pedro";
        var audience = 456;
        var performance = new Performance(playId, audience);

        Assert.Equal(playId, performance.PlayId);
        Assert.Equal(audience, performance.Audience);
    }

    [Fact]
    public void AlteracaoDeDados()
    {
        var performance = new Performance("Dinastia", 32);
        performance.PlayId = "Oscar III";
        performance.Audience = 2275;

        Assert.Equal("Oscar III", performance.PlayId);
        Assert.Equal(2275, performance.Audience);
    }
}