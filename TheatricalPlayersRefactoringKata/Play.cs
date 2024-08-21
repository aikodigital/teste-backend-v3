namespace TheatricalPlayersRefactoringKata;

public class Play
{
    private string _name;
    private int _lines;
    private string _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public string Type { get => _type; set => _type = value; }

    public Play(string name, int lines, string type) {
        this._name = name;
        this._lines = lines;
        this._type = type;
    }
}
