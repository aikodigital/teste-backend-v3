using TheatricalPlayersRefactoringKata.Application.Factory;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Play.Commands.UpdatePlay;

public class UpdatePlayCommandHandler
{
    private readonly IPlayRepository _playRepository;

    UpdatePlayCommandHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public void Handle(UpdatePlayCommand.UpdatePlayCommand command)
    {
        var play = GenreFactory.CreatePlay(command.Name, command.Lines, command.Type);
        _playRepository.CreatePlay(play);
    }
}