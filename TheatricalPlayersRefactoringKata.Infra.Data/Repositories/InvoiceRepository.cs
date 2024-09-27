using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Repositories;

public class InvoiceRepository : BaseRepository<Invoice, ApplicationDbContext>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext context) : base(context)
    {

    }
}
