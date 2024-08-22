using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Play.Commands.CreatePlay;

public class CreatePlayCommand
{
    public string Name { get; private set; }
    public int Lines { get; private set; }
    public EnumGenres Type { get; private set; }

    CreatePlayCommand(string name, int lines, EnumGenres type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}