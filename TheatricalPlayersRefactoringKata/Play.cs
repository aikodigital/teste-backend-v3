using System;

namespace TheatricalPlayersRefactoringKata;

public class Play
{
    private string _name;
    private int _lines;
    private IType _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public IType Type { get => _type; set => _type = value; }

    public Play(string name, int lines, IType type)
    {
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        this._name = name;
        this._lines = lines;
        this._type = type;
    }

    /*
    public double BaseValue()
    {
        return this._lines / 10;
    }
    */

    public double Calculate(int audience)
    {
        return this._type.Calculate(audience, this._lines);
    }

    public int VolumeCredits(int audience)
    {
        return this._type.VolumeCredits(audience);
    }
}
