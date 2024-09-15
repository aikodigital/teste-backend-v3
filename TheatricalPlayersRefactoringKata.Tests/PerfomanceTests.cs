using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class PerfomanceTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateValue_ShouldReturnCorrectAmount()
    {

        var performance = new Performance(new TragedyPlay("The Night Death", 3200), 25); //Valor Esperado : 32000


        var result = performance.CalculateValue();


        Assert.Equal(32000, result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateCredits_ShouldReturnCorrectAmount()
    {

        var performance = new Performance(new TragedyPlay("The Night Death", 3200), 25); //Valor Esperado : 0
        

        var result = performance.CalculateCredits();

        Assert.Equal(0, result);
    }
}
