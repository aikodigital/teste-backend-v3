namespace TheatricalPlayersRefactoringKata.entities;

public class Play(string name, int lines, string type)
{
    public string Name { get; set; } = name;

    public int Lines { get; set; } = lines;

    public string Type { get; set; } = type;
}