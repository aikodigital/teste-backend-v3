using TheatricalPlayersRefactoringKata.Domain.Interface.Repository;

namespace TheatricalPlayersRefactoringKata.Domain.Interface.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        bool HasErrors { get; set; }
        ICustomerRepository CustomerRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IPerformanceRepository PerformanceRepository { get; }
        IPlayRepository PlayRepository { get; }

        IUnitOfWork OpenTransation();
    }
}