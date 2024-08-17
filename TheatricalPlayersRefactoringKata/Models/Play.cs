using TheatricalPlayersRefactoringKata.Enums;

namespace TheatricalPlayersRefactoringKata.Models;

public class Play
{
    private string _name;
    private int _lines;
    private PlayType _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public PlayType Type { get => _type; set => _type = value; }

    public Play(string name, int lines, PlayType type)
    {
        _name = name;
        _lines = lines;
        _type = type;
    }
}
