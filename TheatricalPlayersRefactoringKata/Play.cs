using System;

namespace TheatricalPlayersRefactoringKata;

public class Play
{
    private string _name;
    private int _lines;
    private string _type;
    public string Name { get => _name; set => _name = value; }
    public int Lines
    {
        get => _lines; set
        {
            if (value < 0)
                throw new ArgumentException("Play lines cannot be negative.");
            _lines = value;
        }
    }
    public string Type { get => _type; set => _type = value; }
    public Play(string name, int lines, string type)
    {
        if (lines < 0)
            throw new ArgumentException("Play lines cannot be negative.");

        this._name = name;
        this._lines = lines;
        this._type = type;

    }
}
