namespace TheatricalPlayersRefactoringKata;

public class PlayModel
{
    private string _name;
    private int _lines;
    private string _type;

    public string Name { get => _name; private set => _name = value; }
    public int Lines { get => _lines; private set => _lines = value; }
    public string Type { get => _type; private set => _type = value; }

    public PlayModel(string name, int lines, string type) {
        _name = name;
        _lines = lines;
        _type = type;
    }
}
