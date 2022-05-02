using System;
using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata.Performances;

public class HistoryPerformance : BasePerformance
{
    public override int Audience { get; set; }
    public override IPlay Play { get; set; }

    public override int CalculateAmmount() => throw new NotImplementedException();
    public override int CalculateCredits() => throw new NotImplementedException();

    public HistoryPerformance(IPlay play, int audience) : base(play, audience)
    {
    }
}