using TheatricalPlayersRefactoringKata.Application.Factory;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Invoices.Commands.CreateInvoices;

public class CreateInvoicesCommandHandler
{
    private readonly IPlayRepository _playRepository;

    CreateInvoicesCommandHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public void Handle(CreatePlayCommand command)
    {
        var play = GenreFactory.CreatePlay(command.Name, command.Lines, command.Type);
        _playRepository.CreatePlay(play);
    }
}