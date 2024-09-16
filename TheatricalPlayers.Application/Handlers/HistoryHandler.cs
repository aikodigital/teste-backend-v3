using TheatricalPlayers.Core.Interfaces.Strategies;

namespace TheatricalPlayers.Application.Handlers;

public class HistoryHandler : PlayTypeHandler
{
    private readonly PlayTypeHandler _comedyHandler;
    private readonly PlayTypeHandler _tragedyHandler;
    
    public HistoryHandler(ComedyHandler comedyHandler, TragedyHandler tragedyHandler)
    {
        _comedyHandler = comedyHandler;
        _tragedyHandler = tragedyHandler;
        
        ArgumentNullException.ThrowIfNull(comedyHandler);
        ArgumentNullException.ThrowIfNull(tragedyHandler);
    }
    
    public override int CalculatePriceCents(int lines, int audience)
    {
        var priceCentsComedyPlay = _comedyHandler.CalculatePriceCents(lines, audience);
        var priceCentsTragedyPlay = _tragedyHandler.CalculatePriceCents(lines, audience);

        return priceCentsComedyPlay + priceCentsTragedyPlay;
    }
}