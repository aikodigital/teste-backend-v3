using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class HistoryCalculatorTests
{
    private readonly HistoryCalculator _calculator = new HistoryCalculator();

    [Fact]
    public async Task CalculateAmount_CombinesTragedyAndComedyAmounts()
    {
        var play = new Play("Henry V", 2000, "history");
        var performance = new Performance("henry-v", 30, play);

        decimal result = await _calculator.CalculateAmount(performance, play);

        Assert.Equal(460M, result);  // Valor esperado calculado como soma de tragédia e comédia.
    }

    [Fact]
    public async Task CalculateVolumeCredits_CombinesTragedyAndComedyCredits()
    {
        var play = new Play("Henry V", 3000,"history");
        var performance = new Performance("henry-v", 35, play);

        int result = await _calculator.CalculateVolumeCredits(performance, play);

        Assert.Equal(11, result);  // Créditos esperados calculados como soma de tragédia e comédia.
    }
}

