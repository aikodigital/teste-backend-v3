#region

using TheatricalPlayersRefactoringKata.Core.Entities;

#endregion

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IStatementPrinter
{
    public static abstract string Print(Invoice invoice);
}