using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IStatementFormatter
    {
        //string Format(Invoice invoice, Dictionary<Performance, int> performanceAmounts);
        string Format(Invoice invoice);

    }
}