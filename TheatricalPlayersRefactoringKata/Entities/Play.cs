using System;

namespace TheatricalPlayersRefactoringKata.Entities;

public abstract class Play
{
    private string _name;
    private int _lines;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }

    public abstract decimal CalculateValue(int audience);

    public virtual int CalculateCredits(int audience)
    {
        return Math.Max(audience - 30, 0);

    }

    public Play(string name, int lines)
    {
        _name = name;
        _lines = lines;
    }


}
