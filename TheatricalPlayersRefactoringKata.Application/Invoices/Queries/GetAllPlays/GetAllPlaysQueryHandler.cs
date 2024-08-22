using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.DTOs;

namespace TheatricalPlayersRefactoringKata.Application.Play.Queries.GetAllInvoices;

public class GetAllInvoicesQueryHandler
{
    private readonly IPlayRepository _playRepository;

    public GetAllInvoicesQueryHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public IEnumerable<PlayDTO> Handle(GetAllInvoicesQuery playQuery)
    {
        var plays = _playRepository.GetAllPlays();
            
        var playDtos = plays.Select(play => new PlayDTO(play.Name, play.Lines, play.Type)).ToList();

        return playDtos;
    }
}