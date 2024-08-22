using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Invoices.Commands.UpdateInvoices;

public class UpdateInvoicesCommand
{
    public string Name { get; private set; }
    public int Lines { get; private set; }
    public EnumGenres Type { get; private set; }

    UpdateInvoicesCommand(string name, int lines, EnumGenres type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}