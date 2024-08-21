using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Infra.DataBase.Repository
{
    public class PlayRepository
    {
        private readonly TheatricalContext _context;
        public PlayRepository(IDbContextFactory<TheatricalContext> _factory)
        {
            _context = _factory.CreateDbContext();
        }

        public void CreatePlay(Play play)
        {
            _context.Plays.Add(play);
            _context.SaveChanges();
        }
        public void UpdatePlay(Play play)
        {
            var oldPlay = _context.Plays.FirstOrDefault(x => x.Id == play.Id);
            if (oldPlay != null)
            {
                oldPlay.Id = play.Id;
                oldPlay.Name = play.Name;
            }
            _context.Plays.Update(play);
            _context.SaveChanges();
        }
        public void DeletePlay(Play play)
        {
            var playToRemove = _context.Plays.FirstOrDefault(y => y.Id == play.Id);
            if(playToRemove != null)
            {
                _context.Plays.Remove(play);
                _context.SaveChanges();
            }
        }
        public Play? GetPlayById(int id)
        {
            return _context.Plays.FirstOrDefault(x => x.Id == id);
        }
        public List<Play>? GetAllPlays()
        {
            return _context.Plays.AsNoTracking().ToList();
        }
    }
}
