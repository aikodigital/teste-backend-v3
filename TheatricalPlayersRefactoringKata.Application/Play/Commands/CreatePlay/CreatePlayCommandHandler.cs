using TheatricalPlayersRefactoringKata.Application.Factory;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Play.Commands.CreatePlay;

public class CreatePlayCommandHandler
{
    private readonly IPlayRepository _playRepository;

    CreatePlayCommandHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public void Handle(CreatePlayCommand command)
    {
        var play = GenreFactory.CreatePlay(command.Name, command.Lines, command.Type);
        _playRepository.CreatePlay(play);
    }
}