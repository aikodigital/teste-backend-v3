#region

using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Services;

#endregion

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Play : IPlay
{
    public Play(string name, int lines, Genre type)
    {
        if (lines < 0) throw new Exception("lines cannot be negative");
        Id = Guid.NewGuid();
        Name = name;
        Lines = lines;
        Type = type;
    }
    
    public Play(Guid id, string name, int lines, Genre type)
    {
        if (lines < 0) throw new Exception("lines cannot be negative");
        Id = id;
        Name = name;
        Lines = lines;
        Type = type;
    }
    
    public Guid Id { get; }
    public int Lines { get; }

    public string Name { get; }
    public Genre Type { get; }
    
}