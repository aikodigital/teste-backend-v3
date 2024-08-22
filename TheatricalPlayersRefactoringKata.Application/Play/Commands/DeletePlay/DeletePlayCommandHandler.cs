using TheatricalPlayersRefactoringKata.Application.Factory;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Play.Commands.DeletePlayCommandHandler;

public class DeletePlayCommandHandler
{
    private readonly IPlayRepository _playRepository;

    DeletePlayCommandHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public void Handle(DeletePlayCommand.DeletePlayCommand command)
    {
        _playRepository.DeletePlay(command.Name);
    }
}