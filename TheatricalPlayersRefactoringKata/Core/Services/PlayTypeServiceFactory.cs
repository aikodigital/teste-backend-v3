using System;

public class PlayTypeServiceFactory
{
    public IPlayTypeService Create(string type)
    {
        return type switch
        {
            "tragedy" => new TragedyPlayTypeService(),
            "comedy" => new ComedyPlayTypeService(),
            "historical" => new HistoricalPlayTypeService(),
            _ => throw new ArgumentException($"Unknown play type: {type}")
        };
    }
}
