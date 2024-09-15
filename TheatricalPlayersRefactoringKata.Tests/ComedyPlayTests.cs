using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class ComedyPlayTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateValue_ShouldReturnCorrectAmount()
    {
        var comedyPlay = new ComedyPlay("As You Like It", 3200);
        var result = comedyPlay.CalculateValue(45);

        Assert.Equal(68000, result); // Valor esperado para 45: 68000
    }
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateCredits_ShouldReturnCorrectAmount()
    {
        var comedyPlay = new ComedyPlay("As You Like It", 3200);
        var result = comedyPlay.CalculateCredits(45);

        Assert.Equal(24, result); // Valor esperado para 45: 24
    }
}
