using TheatricalPlayers.Core.Interfaces.Strategies;

namespace TheatricalPlayers.Application.Handlers;

public class ComedyHandler : PlayTypeHandler
{
    public override int CalculatePriceCents(int lines, int audience)
    {
        var priceCents = base.CalculatePriceCents(lines, audience);
        
        priceCents += 300 * audience;
        
        if (audience > 20)
            priceCents += 10000 + 500 * (audience - 20);
        
        return priceCents;
    }

    public override int CalculateCredits(int audience)
    {
        var credits = base.CalculateCredits(audience);
        
        credits += (int)Math.Floor((decimal)audience / 5);

        return credits;
    }
}