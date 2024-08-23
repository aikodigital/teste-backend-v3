using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Repository
{
    public class PlayTypeRepository : IPlayTypeRepository
    {
        private readonly AppDbContext db;

        public PlayTypeRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async void DeleteByName(string typeName)
        {
            PlayType type = await GetByName(typeName);
            db.PlayTypes.Remove(type);
            db.SaveChanges();
        }

        public async Task<IEnumerable<PlayType>> GetAll() => await db.PlayTypes.ToListAsync();

        public async Task<PlayType> Create(PlayType playType)
        {
            await db.PlayTypes.AddAsync(playType);
            await db.SaveChangesAsync();
            return playType;
        }

        public async Task<PlayType> GetByName(string typeName)
        {
            PlayType type = await db.PlayTypes.FirstOrDefaultAsync(p => p.Name.ToLower() == typeName.ToLower());
            return type != null ? type : throw new ArgumentOutOfRangeException($"{typeName} is not a valid type");
        }

        public async Task<PlayType> Update(PlayType playType)
        {
            PlayType type = await db.PlayTypes.AsNoTracking().FirstOrDefaultAsync(e => e.Name.ToLower() == playType.Name.ToLower());

            type.Description = playType.Description;
            type.DtInclusao = playType.DtInclusao;

            db.PlayTypes.Update(playType);
            await db.SaveChangesAsync();
            return type;
        }
    }
}
