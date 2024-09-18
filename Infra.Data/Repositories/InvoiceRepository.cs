using Domain.Entites;
using Domain.Interfaces;
using Infra.Data.Context;
using Infra.Data.Repositories.Base;


namespace Infra.Data.Repositories
{
    public class InvoiceRepository : Repository<InvoiceEntity>, IInvoiceRepository
    {
        public InvoiceRepository(AppSqlLiteContext context) : base(context)
        {
        }
    }

}
