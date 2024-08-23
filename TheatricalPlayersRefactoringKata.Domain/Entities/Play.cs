using System;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata;

public class Play : Entity
{
    private string _name;
    private int _lines;
    private Enum _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public Enum Type { get => _type; set => _type = value; }

    public Play(string name, int lines, Enum type) {
        this._name = name;
        this._lines = lines;
        this._type = type;
    }
}
