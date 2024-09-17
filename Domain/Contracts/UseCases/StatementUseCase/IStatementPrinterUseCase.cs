using Domain.Entities;

namespace Domain.Contracts.UseCases.StatementUseCase
{
    public interface IStatementPrinterUseCase
    {
        string Print(Invoice invoice, List<Play> plays);
    }
}
