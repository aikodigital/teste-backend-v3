namespace TheatricalPlayersRefactoringKata;

public class Play
{
    private string _name;
    private int _lines;
    private string _type;

    public string Name { get => _name; private set => _name = value; }
    public int Lines { get => _lines; private set => _lines = value; }
    public string Type { get => _type; private set => _type = value; }

    public Play(string name, int lines, string type) {
        Name = name;
        Lines = lines;
        Type = type;
    }
}
