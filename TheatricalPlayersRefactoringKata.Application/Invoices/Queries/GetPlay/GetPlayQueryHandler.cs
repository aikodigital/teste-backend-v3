using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.DTOs;

namespace TheatricalPlayersRefactoringKata.Application.Play.Queries.GetInvoices;

public class GetInvoicesQueryHandler
{
    private readonly IPlayRepository _playRepository;

    public GetInvoicesQueryHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public PlayDTO Handle(GetInvoicesQuery playQuery)
    {
        var play = _playRepository.GetPlay(playQuery.Name);

        return new PlayDTO(play.Name, play.Lines, play.Type);
    }
}