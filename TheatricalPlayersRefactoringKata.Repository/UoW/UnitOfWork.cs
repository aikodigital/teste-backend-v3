using TheatricalPlayersRefactoringKata.Domain.Interface.Repository;
using TheatricalPlayersRefactoringKata.Domain.Interface.UoW;
using TheatricalPlayersRefactoringKata.Repository.Configuration.Context;

namespace TheatricalPlayersRefactoringKata.Repository.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public bool HasErrors { get; set; }
        private readonly ITheatricalContext _theatricalContext;
        private readonly IServiceProvider _serviceProvider;

        public ICustomerRepository CustomerRepository => (ICustomerRepository)_serviceProvider.GetService(typeof(ICustomerRepository));
        public IInvoiceRepository InvoiceRepository => (IInvoiceRepository)_serviceProvider.GetService(typeof(IInvoiceRepository));
        public IPerformanceRepository PerformanceRepository => (IPerformanceRepository)_serviceProvider.GetService(typeof(IPerformanceRepository));
        public IPlayRepository PlayRepository => (IPlayRepository)_serviceProvider.GetService(typeof(IPlayRepository));

        public UnitOfWork(ITheatricalContext theatricalContext,
            IServiceProvider serviceProvider)
        {
            _theatricalContext = theatricalContext;
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork OpenTransation()
        {
            if (!HasOpenConnection())
            {
                _theatricalContext.Database.BeginTransaction();
            }

            return this;
        }

        private bool HasOpenConnection()
        {
            bool hasOpenConnection = _theatricalContext.Database.CurrentTransaction != null;

            return hasOpenConnection;
        }

        public void Dispose()
        {
            bool hasOpenConnection = HasOpenConnection();
            if (hasOpenConnection)
            {
                if (this.HasErrors)
                {
                    _theatricalContext.Database.RollbackTransaction();
                }
                else
                {
                    _theatricalContext.Database.CommitTransaction();
                }
            }
        }
    }
}