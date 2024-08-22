using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Infra.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data;

public class PlayRepository : IPlayRepository
{
    private readonly AppDbContext _context;

    public PlayRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Play? GetPlay(string name)
    {
        return _context.Plays.FirstOrDefault(p => p.Name == name);
    }

    public void CreatePlay(Play play)
    {
        _context.Plays.Add(play);
        _context.SaveChanges();
    }

    public void UpdatePlay(Play play)
    {
        var existingPlay = _context.Plays.FirstOrDefault(p => p.Name == play.Name);
        if (existingPlay != null)
        {
            existingPlay = play; // This is a simplistic way to update; in a real application, you might need to update specific properties
            _context.Plays.Update(existingPlay);
            _context.SaveChanges();
        }
    }

    public void DeletePlay(string name)
    {
        var play = _context.Plays.FirstOrDefault(p => p.Name == name);
        if (play != null)
        {
            _context.Plays.Remove(play);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Play> GetAllPlays()
    {
        return _context.Plays.ToList();
    }
}