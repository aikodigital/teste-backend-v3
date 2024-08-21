using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IStatementFormatter
    {
        string Format(Invoice invoice);
    }
}