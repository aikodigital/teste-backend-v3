namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Play
{
    public string Name { get; private set; }
    public int Lines { get; private set; }
    public string Type { get; private set; }

    public Play(string name, int lines, string type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}
