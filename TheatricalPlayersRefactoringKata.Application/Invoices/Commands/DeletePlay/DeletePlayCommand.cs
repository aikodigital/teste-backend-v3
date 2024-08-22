using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Invoices.Commands.DeleteInvoices;

public class DeleteInvoicesCommand
{
    public string Name { get; private set; }

    DeleteInvoicesCommand(string name)
    {
        Name = name;
    }
}