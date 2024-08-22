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
            PlayTypes type = await GetByName(typeName);
            db.PlayTypes.Remove(type);
            db.SaveChanges();
        }

        public async Task<IEnumerable<PlayTypes>> GetAll() => await db.PlayTypes.ToListAsync();

        public async Task<PlayTypes> Create(PlayTypes playType)
        {
            await db.PlayTypes.AddAsync(playType);
            await db.SaveChangesAsync();
            return playType;
        }

        public async Task<PlayTypes> GetByName(string typeName)
        {
            PlayTypes type = await db.PlayTypes.FirstOrDefaultAsync(p => p.Name.ToLower() == typeName.ToLower());
            return type != null ? type : throw new ArgumentOutOfRangeException($"{typeName} is not a valid type");
        }

        public async Task<PlayTypes> Update(PlayTypes playType)
        {
            PlayTypes type = await db.PlayTypes.AsNoTracking().FirstOrDefaultAsync(e => e.Name.ToLower() == playType.Name.ToLower());

            type.Description = playType.Description;
            type.DtInclusao = playType.DtInclusao;

            db.PlayTypes.Update(playType);
            await db.SaveChangesAsync();
            return type;
        }
    }
}
