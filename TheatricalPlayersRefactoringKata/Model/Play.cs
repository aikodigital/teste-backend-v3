namespace TheatricalPlayersRefactoringKata.Model;

public class Play
{
    private string _name;
    private int _lines;
    private Genero _genero;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public Genero Type { get => _genero; set => _genero = value; }

    public Play(string name, int lines, Genero genero)
    {
        _name = name;
        _lines = lines;
        _genero = genero;
    }
}
