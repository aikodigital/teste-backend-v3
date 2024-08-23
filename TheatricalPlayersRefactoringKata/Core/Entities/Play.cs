#region

using System.ComponentModel.DataAnnotations;
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

    public Play(): this("", 0, Genre.Comedy)
    {
    }


    public Guid Id { get; init; }
    public int Lines { get; init; }
    public string Name { get; init; }
    public Genre Type { get; init; }
}