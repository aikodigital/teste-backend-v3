namespace TheatricalPlayers.Core.Interfaces.Strategies;

public abstract class PlayTypeHandler
{
    public virtual int CalculateCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }

    public virtual int CalculatePriceCents(int lines, int audience)
    {
        return Math.Clamp(lines, 1000, 4000) * 10;
    }
}