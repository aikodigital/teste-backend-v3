using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Play : IPlay
{
    public Play(string name, int lines, string type)
    {
        if (lines < 0) throw new Exception("lines cannot be negative");
        Name  = name;
        Lines = lines;
        Type  = type;
    }

    public string Name { get; set; }
    public int Lines { get; set; }
    public string Type { get; set; }
    
    public int CalculateAmount(int audience)
    {
        return Type switch
        {
            "tragedy" => CalculateTragedyAmount(audience) + CalculateDefaultAmount(),
            "comedy"  => CalculateComedyAmount(audience) + CalculateDefaultAmount(),
            "history" => CalculateHistoryAmount(audience),
            _ => CalculateDefaultAmount()
        };
    }

    static private int CalculateTragedyAmount(int audience)
    {
        return audience > 30 ? 1000 * (audience - 30) : 0;
    }

    static private int CalculateComedyAmount(int audience)
    {
        var baseAmount = 300 * audience;
        return audience > 20 ? (10000 + 500 * (audience - 20)) + baseAmount : baseAmount;
    }

    private int CalculateHistoryAmount(int audience)
    {
        var comedyAmount = CalculateComedyAmount(audience) + CalculateDefaultAmount();
        var tragedyAmount = CalculateTragedyAmount(audience) + CalculateDefaultAmount();
        var historyAmount = comedyAmount + tragedyAmount;
        return historyAmount;
    }

    private int CalculateDefaultAmount()
    {
        return Lines switch {
            < 1000 => 1000 * 10,
            > 4000 => 4000 * 10,
            _ => Lines * 10
        };
    }
}