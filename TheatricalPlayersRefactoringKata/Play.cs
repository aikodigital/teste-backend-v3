using System;
namespace TheatricalPlayersRefactoringKata;

//Peça de teatro
public abstract class Play : IPlay
{
    private const int AUDIENCE_FROM_AT_LEAST_FOR_CREDIT = 30;

    public Guid Guid { get; private set; }

    private const int LINE_MIN = 1000;
    private const int LINE_MAX = 4000;

    private int _baseValue;

    public string Name { get; protected set; }
    public int Lines { get; protected set; }
    public int BaseValue { get => _baseValue; }

    public abstract int CalculateBaseValue(Performance performance);
    protected abstract int CalculateCredits(int audience);

    public Play(string name, int lines)
    {
        Name = name;
        Lines = lines;
        _baseValue = GetLines() / 10;
        Guid = Guid.NewGuid();
    }

    public int GetCredits(int audience)
    {
        var defaultCredits = Math.Max(audience - AUDIENCE_FROM_AT_LEAST_FOR_CREDIT, 0);

        return defaultCredits + CalculateCredits(audience);
    }


    public void SumBaseValue(int value)
    {
        _baseValue += value;
    }
    

    public int GetLines()
    {
        var lines = Lines;

        if (Lines < LINE_MIN) lines = LINE_MIN;
        if (Lines > LINE_MAX) lines = LINE_MAX;

        return lines;
    }

    
}
