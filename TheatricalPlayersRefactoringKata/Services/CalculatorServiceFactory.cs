using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public static class CalculatorServiceFactory
{
    public static ICalculator Create(Gender type)
    {
        return type switch
        {
            Gender.Tragedy => new TragedyCalculatorService(),
            Gender.Comedy => new ComedyCalculatorService(),
            Gender.History => new HistoryCalculatorService(),
            _ => throw new Exception("Unknown gender type"),
        };
    }
}
