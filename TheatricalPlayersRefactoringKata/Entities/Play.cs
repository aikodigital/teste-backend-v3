using System;
using System.Runtime.InteropServices;
using TheatricalPlayersRefactoringKata.Categorys;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Play
{
    private string _name;
    private int _lines;
    private IType _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public IType Type { get => _type; set => _type = value; }


    //Constructor with conditional on the lines attribute
    public Play(string name, int lines, IType type)
    {
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        _name = name;
        _lines = lines;
        _type = type;
    }

    //Calculates the charge amount according to type
    public double Calculate(int audience)
    {
        return _type.Calculate(audience, _lines);
    }
    //Calculates credits according to type
    public int VolumeCredits(int audience)
    {
        return _type.VolumeCredits(audience);
    }
}
