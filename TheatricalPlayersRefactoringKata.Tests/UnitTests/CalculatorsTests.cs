using System;
using TheatricalPlayersRefactoringKata.Model;
using TheatricalPlayersRefactoringKata.Service.Calculators;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UnitTests;

public class CalculatorsTests
{
    [Fact]
    public void TragedyGenreCalculatorTest()
    {
        Play play = new Play("Hamlet", 4024, Genre.tragedy);
        Performance perf = new Performance("hamlet", 55);
        
        StatementCalculator statementCalculator = new StatementCalculator(new TragedyGenreCalculator());

        var amountOwed = statementCalculator.CalculateAmountOwned(perf, play);
        var earnedCredits = statementCalculator.CalculateCredits(perf.Audience);

        Assert.Equal((decimal)650.00, Convert.ToDecimal(amountOwed) / 100);
        Assert.Equal((uint)25, earnedCredits);
    }

    [Fact]
    public void ComedyGenreCalculatorTest()
    {
        Play play = new Play("As You Like It", 2670, Genre.comedy);
        Performance perf = new Performance("as-like", 35);
        
        StatementCalculator statementCalculator = new StatementCalculator(new ComedyGenreCalculator());

        var amountOwed = statementCalculator.CalculateAmountOwned(perf, play);
        var earnedCredits = statementCalculator.CalculateCredits(perf.Audience);

        Assert.Equal((decimal)547.00, Convert.ToDecimal(amountOwed) / 100);
        Assert.Equal((uint)12, earnedCredits);
    }

    [Fact]
    public void HistoryGenreCalculatorTest()
    {
        Play play = new Play("King John", 2648, Genre.history);
        Performance perf = new Performance("john", 39);
        
        StatementCalculator statementCalculator = new StatementCalculator(new HistoryGenreCalculator());

        var amountOwed = statementCalculator.CalculateAmountOwned(perf, play);
        var earnedCredits = statementCalculator.CalculateCredits(perf.Audience);

        Assert.Equal((decimal)931.60, Convert.ToDecimal(amountOwed) / 100);
        Assert.Equal((uint)9, earnedCredits);
    }
}
