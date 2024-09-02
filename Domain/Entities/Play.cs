using Domain.Enums;

namespace Domain.Entities;

public class Play
{
    public int Id { get; set; }
    private string _name;
    private int _lines;
    private Types _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public Types Type { get => _type; set => _type = value; }

    public Play(string name, int lines, Types type)
    {
        _name = name;
        _lines = lines;
        _type = type;
    }
}
