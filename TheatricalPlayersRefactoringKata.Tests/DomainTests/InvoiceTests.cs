using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.DomainTests;

public class InvoiceTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateTotals_ShouldReturnCorrectAmount()
    {

        var performances = new List<Performance>
        {
            new Performance(new TragedyPlay("The Night Death", 3200), 25), //Valor Esperado : 32000
            new Performance(new ComedyPlay("Two is always better", 3200), 45), //Valor Esperado : 68000
            new Performance(new HistoricalPlay("Roma", 3200), 45) //Valor Esperado : 115000
        };
        var invoice = new Invoice("UnitTest1", performances);


        var result = invoice.CalculateTotals();


        Assert.Equal(215000, result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CalculateCredits_ShouldReturnCorrectAmount()
    {

        var performances = new List<Performance>
        {
            new Performance(new TragedyPlay("The Night Death", 3200), 25), //Valor Esperado : 0
            new Performance(new ComedyPlay("Two is always better", 3200), 45), //Valor Esperado : 24
            new Performance(new HistoricalPlay("Roma", 3200), 45) //Valor Esperado : 15
        };
        var invoice = new Invoice("UnitTest2", performances);


        var result = invoice.CalculateCredits();

        Assert.Equal(39, result);
    }

}
