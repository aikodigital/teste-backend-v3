using TheatricalPlayersRefactoringKata.Domain.Repositories;

namespace TheatricalPlayersRefactoringKata.Infraestructure.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CashFlowDbContext _dbContext;
        public UnitOfWork(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
