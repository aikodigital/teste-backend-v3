using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata.Performances;

public abstract class BasePerformance : IPerformance
{
    public abstract int Audience { get; set; }
    public abstract IPlay Play { get; set; }

    public abstract decimal CalculateAmmount(decimal baseAmount);
    public abstract int CalculateCredits();

    protected BasePerformance(IPlay play, int audience)
    {
        Audience = audience;
        Play = play;
    }
}