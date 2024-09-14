using TheatricalPlayers.Core.Interfaces.Strategies;

namespace TheatricalPlayers.Application.Handlers;

public class TragedyHandler : PlayTypeHandler
{
    public override int CalculatePriceCents(int lines, int audience)
    {
        var priceCents = base.CalculatePriceCents(lines, audience);
        
        if (audience > 30)
            priceCents += 1000 * (audience - 30);
        
        return priceCents;
    }
}