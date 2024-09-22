using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class HistoryCalculatorService : ICalculator
{
    public int CalculateAmount(Performance performance, Play play)
    {
        // Valor da peça histórica é a soma dos valores da tragédia e comédia
        var tragedyAmount = new TragedyCalculatorService().CalculateAmount(performance, play);
        var comedyAmount = new ComedyCalculatorService().CalculateAmount(performance, play);

        return tragedyAmount + comedyAmount;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        // Créditos da peça histórica é a soma dos créditos da tragédia e comédia
        var tragedyCredits = new TragedyCalculatorService().CalculateCredits(performance, play);
        var comedyCredits = new ComedyCalculatorService().CalculateCredits(performance, play);

        return tragedyCredits + comedyCredits;
    }
}
