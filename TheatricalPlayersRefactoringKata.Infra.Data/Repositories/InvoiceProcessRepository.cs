using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Repositories;

public class InvoiceProcessRepository : BaseRepository<InvoiceProcess, ApplicationDbContext>, IInvoiceProcessRepository
{
    public InvoiceProcessRepository(ApplicationDbContext context) : base(context)
    {

    }
}