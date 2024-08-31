using System;
using TheatricalPlayersRefactoringKata.Core.Enuns;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Play
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public Genre Type { get; set; }
    public int Lines { get; set; }

    public Play(string name, int lines, Genre type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}
