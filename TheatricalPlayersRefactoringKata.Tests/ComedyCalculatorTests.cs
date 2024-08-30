using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services.Gender;
using TheatricalPlayersRefactoringKata.Core.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class ComedyCalculatorTests
{
    private readonly ComedyCalculator _calculator = new ComedyCalculator();
    [Fact]
    public async Task CalculateAmount_AudienceUnder20_ReturnsCorrectAmount()
    {
        var play = new Play("As You Like It", 1500, "comedy");
        var performance = new Performance("as-like", 15, play);

        decimal result = await _calculator.CalculateAmount(performance, play);

        Assert.Equal(195M, result);  // Valor esperado ajustado conforme cálculo manual
    }

    [Fact]
    public async Task CalculateAmount_AudienceAbove20_ReturnsCorrectAmount()
    {
        var play = new Play("As You Like It", 2000, "comedy");
        var performance = new Performance("as-like", 30, play);

        decimal result = await _calculator.CalculateAmount(performance, play);

        Assert.Equal(440M, result);  // Valor esperado ajustado conforme cálculo manual
    }

    [Fact]
    public async Task CalculateVolumeCredits_AudienceAbove20_ReturnsCorrectCredits()
    {
        var play = new Play("As You Like It", 2000, "comedy");
        var performance = new Performance("as-like", 30, play);

        int result = await _calculator.CalculateVolumeCredits(performance, play);

        Assert.Equal(6, result);  // Créditos esperados calculados conforme regras de negócio
    }

}
