using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Repositories;

public class CustomerStatementRepository : BaseRepository<CustomerStatement, ApplicationDbContext>, ICustomerStatementRepository
{ 
    public CustomerStatementRepository(ApplicationDbContext context) : base(context)
    {

    }
}
