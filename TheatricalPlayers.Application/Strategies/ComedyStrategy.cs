using TheatricalPlayers.Core.Interfaces.Strategies;

namespace TheatricalPlayers.Application.Strategies;

public class ComedyStrategy : IPlayTypeStrategy
{
    public int CalculateAmount(int lines, int audience)
    {
        var amount = Math.Clamp(lines, 1000, 4000) * 10;
        if (audience > 20)
            amount += 10000 + 500 * (audience - 20);
        amount += 300 * audience;
        return amount;
    }

    public int CalculateVolumeCredits(int audience)
    {
        var credits = Math.Max(audience - 30, 0);
        
        credits += (int)Math.Floor((decimal)audience / 5);

        return credits;
    }
}