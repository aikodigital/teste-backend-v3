using TheatricalPlayersRefactoringKata.Application.Factory;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Invoices.Commands.UpdateInvoices;

public class UpdatePlayCommandHandler
{
    private readonly IPlayRepository _playRepository;

    UpdatePlayCommandHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public void Handle(UpdateInvoicesCommand command)
    {
        var play = GenreFactory.CreatePlay(command.Name, command.Lines, command.Type);
        _playRepository.CreatePlay(play);
    }
}