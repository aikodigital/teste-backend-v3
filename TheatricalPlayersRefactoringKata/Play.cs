using System;
namespace TheatricalPlayersRefactoringKata;

//Peça de teatro
public abstract class Play : IPlay
{
    private const int LINE_MIN = 1000;
    private const int LINE_MAX = 4000;

    private int _baseValue;

    public string Name { get; protected set; }
    public int Lines { get; protected set; }
    public int BaseValue { get => _baseValue; }

    
    public abstract int CalculateBaseValue(Performance performance);

    public Play(string name, int lines)
    {
        Name = name;
        Lines = lines;

        _baseValue = GetLines() / 10;
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
