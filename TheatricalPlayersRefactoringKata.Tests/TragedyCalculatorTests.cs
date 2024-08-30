using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services.Gender;
using TheatricalPlayersRefactoringKata.Core.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class TragedyCalculatorTests
{
    private readonly TragedyCalculator _calculator = new TragedyCalculator();

    [Fact]
    public async Task CalculateAmount_AudienceUnder30_ReturnsCorrectAmount()
    {
        var play = new Play("Hamlet", 1500, "tragedy");
        var performance = new Performance("hamlet", 25, play);

        decimal result = await _calculator.CalculateAmount(performance, play);

        Assert.Equal(150M, result);  // Valor esperado calculado manualmente.
    }

    [Fact]
    public async Task CalculateAmount_AudienceAbove30_ReturnsCorrectAmount()
    {
        var play = new Play("Hamlet", 3500, "tragedy");
        var performance = new Performance("hamlet", 40, play);

        decimal result = await _calculator.CalculateAmount(performance, play);

        Assert.Equal(450M, result);  // Valor corrigido para 450M.
    }

    [Fact]
    public async Task CalculateVolumeCredits_AudienceAbove30_ReturnsCorrectCredits()
    {
        var play = new Play("Hamlet", 2000, "tragedy");
        var performance = new Performance("hamlet", 35, play);

        int result = await _calculator.CalculateVolumeCredits(performance, play);

        Assert.Equal(5, result);  // Créditos esperados calculados manualmente.
    }
}
