using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class TragedyCalculatorService : CalculatorService
{
    public override int CalculateAmount(Performance performance, Play play)
    {
        var amount = CalculateBaseAmount(play.Lines);
        
        if (performance.Audience > 30)
        {
            amount += 1000 * (performance.Audience - 30);
        }

        return amount;
    }
}
