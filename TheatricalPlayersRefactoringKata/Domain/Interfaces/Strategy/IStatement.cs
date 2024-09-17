using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy
{
    public interface IStatement
    {
        string Print(Invoice invoice);
    }
}
