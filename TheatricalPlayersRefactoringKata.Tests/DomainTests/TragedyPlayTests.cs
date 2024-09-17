using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Tests.DomainTests;

public class TragedyPlayTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateValue_ShouldReturnCorrectAmount()
    {
        var tragedyPlay = new TragedyPlay("The Night Death", 3200);
        var result = tragedyPlay.CalculateValue(25);

        Assert.Equal(32000, result); // Valor esperado para 25: 32000
    }
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateCredits_ShouldReturnCorrectAmount()
    {
        var tragedyPlay = new TragedyPlay("The Night Death", 3200);
        var result = tragedyPlay.CalculateCredits(25);

        Assert.Equal(0, result); // Valor esperado para 25: 0
    }

}
