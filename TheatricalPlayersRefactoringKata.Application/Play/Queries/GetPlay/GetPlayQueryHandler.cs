using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.DTOs;

namespace TheatricalPlayersRefactoringKata.Application.Play.Queries.GetPlay;

public class GetPlayQueryHandler
{
    private readonly IPlayRepository _playRepository;

    public GetPlayQueryHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public PlayDTO Handle(GetPlayQuery playQuery)
    {
        var play = _playRepository.GetPlayById(playQuery.Id);

        return new PlayDTO(play.Name, play.Lines, play.Type);
    }
}