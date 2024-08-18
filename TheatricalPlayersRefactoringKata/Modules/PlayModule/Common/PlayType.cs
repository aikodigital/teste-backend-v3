using TheatricalPlayersRefactoringKata.Modules;

public interface PlayType
{
    decimal CalculateAmount(Performance performance, Play play);
    decimal CalculateCredit(Performance performance, Play play);
}

public abstract class AbstractPlayType : PlayType
{
    public virtual decimal CalculateAmount(Performance performance, Play play) { return (decimal)Math.Clamp(play.Lines, 1000, 4000) / 10; }

    public virtual decimal CalculateCredit(Performance performance, Play play) { return Math.Max(performance.Audience - 30, 0); }
}