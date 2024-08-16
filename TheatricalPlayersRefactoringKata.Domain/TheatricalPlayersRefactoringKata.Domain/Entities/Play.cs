using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Play
{
    public Play()
    {
        
    }
    private long _id;
    private string _name;
    private int _lines;
    private string _type;

    public long Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }

    public PlayTypes Type { get; set; }

    public Play(string name, int lines, long id)
    {
        _id = id;
        _name = name;
        _lines = lines;
        _type = Type.ToString();
    }
}
