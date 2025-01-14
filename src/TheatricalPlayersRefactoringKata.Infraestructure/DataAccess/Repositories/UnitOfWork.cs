using TheatricalPlayersRefactoringKata.Domain.Repositories;

namespace TheatricalPlayersRefactoringKata.Infraestructure.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TheatricalPlayersRefactoringKataDbContext _dbContext;
        public UnitOfWork(TheatricalPlayersRefactoringKataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
