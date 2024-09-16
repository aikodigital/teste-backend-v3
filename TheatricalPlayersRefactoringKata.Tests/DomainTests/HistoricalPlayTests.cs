using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata.Entities;
namespace TheatricalPlayersRefactoringKata.Tests.DomainTests;

public class HistoricalPlayTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateValue_ShouldReturnCorrectAmount()
    {
        var historicalPlay = new HistoricalPlay("Roma", 3200);
        var result = historicalPlay.CalculateValue(45);

        Assert.Equal(115000, result); // Valor esperado para 45: 115000
    }
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateCredits_ShouldReturnCorrectAmount()
    {
        var historicalPlay = new HistoricalPlay("Roma", 3200);
        var result = historicalPlay.CalculateCredits(45);

        Assert.Equal(15, result); // Valor esperado para 45: 15
    }

}
