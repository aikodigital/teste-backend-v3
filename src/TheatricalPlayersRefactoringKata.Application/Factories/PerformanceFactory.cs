using TheatricalPlayersRefactoringKata.Application.Strategies.Calculators;
using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Application.Factories;

public static class PerformanceFactory
{
    public static IPlayCalculatorStrategy CreateStrategy(Enum genre)
    {
        return genre switch
        {
            GenreEnum.Tragedy => new TragedyCalculatorStrategy(),
            GenreEnum.Comedy => new ComedyCalculatorStrategy(),
            GenreEnum.Historical => new HistoricalCalculatorStrategy(),
            _ => throw new ArgumentException($"Invalid performance {nameof(GenreEnum)}")
        };
    }
}

