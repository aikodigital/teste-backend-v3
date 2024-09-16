namespace TheatricalPlayersRefactoringKata.Entities;

public class Play
{
    private int _id;
    private string _name;
    private int _lines;
    private string _type;

    public int Id { get => _id; }
    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public string Type { get => _type; set => _type = value; }

    public Play(string name, int lines, string type)
    {
        _name = name;
        _lines = lines;
        _type = type;
    }

    public int CalculateBaseAmount()
    {
        var lines = _lines;
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        return lines * 10;
    }
}
