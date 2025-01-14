using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories.Plays;

namespace TheatricalPlayersRefactoringKata.Infraestructure.DataAccess.Repositories
{
    public class PlaysRepository : IPlaysWriteOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext;
        public PlaysRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Play play)
        {
            await _dbContext.Play.AddAsync(play);
        }
    }
}
