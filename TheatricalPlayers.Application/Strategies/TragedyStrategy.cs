using TheatricalPlayers.Core.Interfaces.Strategies;

namespace TheatricalPlayers.Application.Strategies;

public class TragedyStrategy : IPlayTypeStrategy
{
    public int CalculateAmount(int lines, int audience)
    {
        var amount = Math.Clamp(lines, 1000, 4000) * 10;
        if (audience > 30)
            amount += 1000 * (audience - 30);
        return amount;
    }

    public int CalculateVolumeCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }
}