namespace TheatricalPlayersRefactoringKata.Modules;

public class Play
{
    private string _name;
    private int _lines;
    private AbstractPlayType _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public AbstractPlayType Type { get => _type; set => _type = value; }

    public Play(string name, int lines, AbstractPlayType type)
    {
        _name = name;
        _lines = lines;
        _type = type;
    }
}