using Domain.Contracts.Repositories.InvoiceRepository;
using Domain.Entities;
using Infra.Context;

namespace Infra.Repository.Repositories.InvoiceRepository
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext context) : base(context)
        {

        }
    }
}
