namespace TheatricalPlayersRefactoringKata;

public class Play
{
    private string _name;
    private int _lines;
    private TypePlay _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public TypePlay Type { get => _type; set => _type = value; }

    public Play(string name, int lines, TypePlay type) 
    {
        _name = name;
        _lines = lines;
        _type = type;
    }

    public enum TypePlay
    {
        Tragedy,
        Comedy,
        History,
    }
}
