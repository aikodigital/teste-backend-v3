namespace TheatricalPlayersRefactoringKata.Model;

public class Play
{
    private int _id;
    private string _name;
    private uint _lines;
    private Genre _genre;

    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public uint Lines { get => _lines; set => _lines = value; }
    public Genre Genre { get => _genre; set => _genre = value; }

    public Play(string name, uint lines, Genre genre) 
    {
        _name = name;
        _lines = lines;
        _genre = genre;
    }
}
