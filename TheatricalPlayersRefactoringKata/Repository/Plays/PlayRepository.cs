using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Repository.Plays
{
    public class PlayRepository : IPlayRepository
    {
        private readonly AppDbContext db;

        public PlayRepository(AppDbContext db) => this.db = db;

        public async Task<Play> Create(Play playType)
        {
            await db.Plays.AddAsync(playType);
            await db.SaveChangesAsync();
            return playType;
        }

        public async void DeleteByName(string playName)
        {
            var play = await GetByName(playName);
            db.Plays.Remove(play);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Play>> GetAll() => await db.Plays.ToArrayAsync();

        public async Task<Play> GetByName(string playName)
        {
            playName = playName.TrimEnd(' ').Replace(" ", "-").ToLower();
            Play play = await db.Plays
                                .AsNoTracking()
                                .Where(p => p.Name.Replace(" ", "-").ToLower().Contains(playName))
                                .FirstOrDefaultAsync();

            return play != null ? play : throw new ArgumentOutOfRangeException($"{playName} is not a valid play");
        }

        public async Task<Play> Update(Play playNew)
        {
            Play play = await GetByName(playNew.Name);

            play.Lines = playNew.Lines;
            play.Type = playNew.Type;

            db.Plays.Update(play);
            await db.SaveChangesAsync();
            return play;
        }
    }
}
