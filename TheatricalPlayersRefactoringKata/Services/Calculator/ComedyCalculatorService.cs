using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public class ComedyCalculatorService : CalculatorService
{
    public override int CalculateAmount(Performance performance, Play play)
    {
        var amount = CalculateBaseAmount(play.Lines);

        if (performance.Audience > 20)
        {
            amount += 10000 + 500 * (performance.Audience - 20);
        }

        amount += 300 * performance.Audience;

        return amount;
    }

    public override int CalculateCredits(Performance performance, Play play) =>
        base.CalculateCredits(performance, play) + (int)Math.Floor((decimal)performance.Audience / 5);
}
