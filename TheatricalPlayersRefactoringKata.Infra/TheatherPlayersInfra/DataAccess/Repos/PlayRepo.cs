using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repos;

namespace TheatherPlayersInfra.DataAccess.Repos;

internal class PlayRepo : IPlay
{
    private readonly TheatherPlayersDbContext _dbContext;
    public PlayRepo(TheatherPlayersDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Play play)
    {
        await _dbContext.Plays.AddAsync(play);
    }
    public async Task<List<Play>> GetAllPlays()
    {
        return await _dbContext.Plays.AsNoTracking().ToListAsync();
    }

    public async Task<Play?> GetByPlay(string name)
    {
        return await _dbContext.Plays
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Name == name);
    }

}

