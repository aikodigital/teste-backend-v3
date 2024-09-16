using Domain.Entities;

namespace Domain.Contracts.UseCases.StatementUseCase
{
    public interface IStatementPrinterUseCase
    {
        string Print(Invoice invoice, Dictionary<string, Play> plays);
    }
}
