namespace TheatricalPlayersRefactoringKata.Entities;

public class PlayEntity
{
    public string Name { get; private set; }

    public int Lines { get; private set; }

    public string Type { get; private set; }

    public PlayEntity(string name, int lines, string type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}