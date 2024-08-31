using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statment.Print;

public interface IStatementPrinter
{
    public static abstract string Print(Invoice invoice);
}
