using System;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Play
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Lines { get; set; }
    public string Type { get; set; }

    public Play()
    {
        Id = Guid.NewGuid().ToString();
    }

    public Play(string name, int lines, string type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}
