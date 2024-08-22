using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.DTOs;

namespace TheatricalPlayersRefactoringKata.Application.Play.Queries.GetAllPlay;

public class GetAllPlayQueryHandler
{
    private readonly IPlayRepository _playRepository;

    public GetAllPlayQueryHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public IEnumerable<PlayDTO> Handle(GetAllPlayQuery playQuery)
    {
        var plays = _playRepository.GetAllPlays();
            
        var playDtos = plays.Select(play => new PlayDTO(play.Name, play.Lines, play.Type)).ToList();

        return playDtos;
    }
}