using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Repositories;

public class CustomerPlaysStatementRepository : BaseRepository<CustomerPlaysStatement, ApplicationDbContext>, ICustomerPlaysStatementRepository
{
    public CustomerPlaysStatementRepository(ApplicationDbContext context) : base(context)
    {

    }
}
