using TP.Domain.Entities;
using TP.Infrastructure.Data;

public interface IPlayRepository
{
    IEnumerable<Play> GetAllPlays();
}

public class PlayRepository : IPlayRepository
{
    private readonly ApplicationDbContext _context;

    public PlayRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Play> GetAllPlays()
    {
        return _context.Plays.ToList();
    }
}