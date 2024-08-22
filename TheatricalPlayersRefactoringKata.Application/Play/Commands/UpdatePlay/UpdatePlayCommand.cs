using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Play.Commands.UpdatePlayCommand;

public class UpdatePlayCommand
{
    public string Name { get; private set; }
    public int Lines { get; private set; }
    public EnumGenres Type { get; private set; }

    UpdatePlayCommand(string name, int lines, EnumGenres type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}