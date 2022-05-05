using System;
using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata.Performances;

public class ComedyPerformance : BasePerformance
{
    public override int Audience { get; set; }
    public override IPlay Play { get; set; }

    public override decimal CalculateAmmount(decimal baseAmount)
    {
        decimal calc = 3.00m * Audience;

        if (Audience > 20)
            calc += 100 + (5.00m * (Audience - 20));

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
