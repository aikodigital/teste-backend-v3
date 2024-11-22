using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
{
    public class PlayRepository : IPlayRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Play> AddAsync(Domain.Entities.Play play)
        {
            _context.Plays.Add(play);
            await _context.SaveChangesAsync();
            return play;
        }
    }
}
