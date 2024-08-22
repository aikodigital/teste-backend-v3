using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Play.Commands.DeletePlayCommand;

public class DeletePlayCommand
{
    public string Name { get; private set; }

    DeletePlayCommand(string name)
    {
        Name = name;
    }
}