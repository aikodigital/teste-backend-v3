using System;
using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata.Performances;

public class HistoryPerformance : BasePerformance
{
    public override int Audience { get; set; }
    public override IPlay Play { get; set; }

    public override int CalculateAmmount()
    {
        var tragedy = new TragedyPerformance(Play, Audience);

        var comedy = new ComedyPerformance(Play, Audience);

        return tragedy.CalculateAmmount() + comedy.CalculateAmmount();
    }

    public override int CalculateCredits()
    {
        return 0;
    }

    public HistoryPerformance(IPlay play, int audience) : base(play, audience)
    {
    }
}