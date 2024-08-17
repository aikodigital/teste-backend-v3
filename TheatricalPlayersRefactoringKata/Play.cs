namespace TheatricalPlayersRefactoringKata;

public class Play
{
    private string _name;
    private int _lines;
    private string _genre;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public string Genre { get => _genre; set => _genre = value; }

    public Play(string name, int lines, string genre) {
        this._name = name;
        this._lines = lines;
        this._genre = genre;
    }
}
