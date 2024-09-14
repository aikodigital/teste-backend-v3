using TheatricalPlayersRefactoringKata.Infrastructure.Context;
using TheatricalPlayersRefactoringKata.Interface;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TheatricalContext _context;
        public UnitOfWork(TheatricalContext context)
        {
            _context = context;
        }
        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
