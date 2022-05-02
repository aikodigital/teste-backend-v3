using System;
using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata.Performances;

public class ComedyPerformance : BasePerformance
{
    public override int Audience { get; set; }
    public override IPlay Play { get; set; }

    public override int CalculateAmmount()
    {
        int calc = 0;
        if (Audience > 20)
            calc += 10000 + 500 * (Audience - 20);
        calc += 300 * Audience;

        return calc;
    }

    public override int CalculateCredits()
    {
        return (int)Math.Floor((decimal)Audience / 5);
    }

    public ComedyPerformance(IPlay play, int audience) : base(play, audience)
    {
    }
}
