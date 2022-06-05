using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Interface.Repository;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;
using TheatricalPlayersRefactoringKata.Repository.Configuration.Context;

namespace TheatricalPlayersRefactoringKata.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ITheatricalContext theatricalContext) : base(theatricalContext)
        {

        }
        public override async Task<Invoice> GetAsync(long id)
        {
            Invoice invoice = DbSet.Include(obj => obj.Customer)
                                   .Include(obj => obj.Performances)
                                   .ThenInclude(obj => obj.Play)
                                   .FirstOrDefault(data => data.Id == id);

            return invoice;
        }
    }
}