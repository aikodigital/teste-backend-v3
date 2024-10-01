using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Repositories;

public class CustomerStatementProcessRepository : BaseRepository<CustomerStatementProcess, ApplicationDbContext>, ICustomerStatementProcessRepository
{
    public CustomerStatementProcessRepository(ApplicationDbContext context) : base(context)
    {

    }
}