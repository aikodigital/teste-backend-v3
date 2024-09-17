namespace Domain.Entities;

public class Play
{
    private string _nameId;
    private string _name;
    private int _lines;
    private string _type;

    public string NameId { get => _nameId; set => _nameId = value; }
    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public string Type { get => _type; set => _type = value; }

    public Play(string nameId, string name, int lines, string type)
    {
        _name = name;
        _nameId = nameId;
        _lines = lines;
        _type = type;
    }
}

