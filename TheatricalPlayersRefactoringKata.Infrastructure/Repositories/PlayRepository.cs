using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
{
    public class PlayRepository : IPlayRepository
    {
        private readonly AppDbContext _context;

        public PlayRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatePlay(Play play)
        {
            _context.Plays.Add(play);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Play>> GetPlays()
        {
            return await _context.Plays
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Play?> GetPlayById(Guid playId)
        {
            return await _context.Plays
                .FirstOrDefaultAsync(p => p.Id == playId);
        }
    }
}
