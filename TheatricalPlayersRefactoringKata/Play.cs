using System;
namespace TheatricalPlayersRefactoringKata;

public abstract class Play : IPlay
{
    private const int AUDIENCE_FROM_AT_LEAST_FOR_CREDIT = 30;
    private const int LINE_MIN = 1000;
    private const int LINE_MAX = 4000;

    private decimal _baseValue;

    public string Name { get; protected set; }
    public int Lines { get; protected set; }
    public decimal BaseValue { get => _baseValue; }
    
    public Guid Guid { get; private set; }

    public abstract decimal CalculateBaseValue(int audience);
    protected abstract int CalculateCredits(int audience);

    public Play(string name, int lines)
    {
        Name = name;
        Lines = lines;
        _baseValue = GetLines() / 10M;
        Guid = Guid.NewGuid();
    }

    public int GetCredits(int audience)
    {
        var defaultCredits = Math.Max(audience - AUDIENCE_FROM_AT_LEAST_FOR_CREDIT, 0);

        return defaultCredits + CalculateCredits(audience);
    }


    public void SumBaseValue(decimal value)
    {
        _baseValue += value;
    }
    

    public decimal GetLines()
    {
        var lines = Lines;

        if (Lines < LINE_MIN) lines = LINE_MIN;
        if (Lines > LINE_MAX) lines = LINE_MAX;

        return lines;
    }

    
}
