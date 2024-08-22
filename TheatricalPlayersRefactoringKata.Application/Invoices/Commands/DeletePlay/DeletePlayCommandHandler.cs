using TheatricalPlayersRefactoringKata.Application.Factory;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Invoices.Commands.DeleteInvoices;

public class DeleteInvoicesCommandHandler
{
    private readonly IPlayRepository _playRepository;

    DeleteInvoicesCommandHandler(IPlayRepository playRepository)
    {
        _playRepository = playRepository;
    }

    public void Handle(DeleteInvoicesCommand command)
    {
        _playRepository.DeletePlay(command.Name);
    }
}