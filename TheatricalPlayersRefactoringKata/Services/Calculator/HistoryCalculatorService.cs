using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class HistoryCalculatorService : CalculatorService
{
    public override int CalculateAmount(Performance performance, Play play)
    {
        var tragedyAmount = new TragedyCalculatorService().CalculateAmount(performance, play);
        var comedyAmount = new ComedyCalculatorService().CalculateAmount(performance, play);

        return tragedyAmount + comedyAmount;
    }
}
