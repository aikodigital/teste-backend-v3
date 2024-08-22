using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IStatementPrinter
{
    public static abstract string Print(Invoice invoice);
}