using MongoDB.Bson;
using MongoDB.Driver;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interface;

namespace TheatricalPlayersRefactoringKata.Infra.Repositories
{
    public class PlayRepository : IPlayRepository
    {
        private readonly MongoContext _mongoContext;

        public PlayRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<bool> Create(Play play)
        {
            try
            {
                await _mongoContext.GetCollection<Play>().InsertOneAsync(play);

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out ObjectId objId))
                    return default;

                var filterId = Builders<Play>.Filter.Eq("_id", objId);

                await _mongoContext.GetCollection<Play>().DeleteOneAsync(filterId);

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Play>> GetAll()
        {
            try
            {
                var filterId = Builders<Play>.Filter.Ne(x => x.Name, string.Empty);

                var result = await _mongoContext.GetCollection<Play>().Find(filterId).ToListAsync();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Play> GetByName(string name)
        {
            try
            {
                var filterId = Builders<Play>.Filter.Eq(x => x.Name, name);

                var result = await _mongoContext.GetCollection<Play>().Find(filterId).FirstOrDefaultAsync();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Play> Update(Play play, string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out ObjectId objId))
                    return default;

                var filterId = Builders<Play>.Filter.Eq("_id", objId);

                await _mongoContext.GetCollection<Play>().FindOneAndReplaceAsync(filterId, play);

                return play;
            }
            catch
            {
                throw;
            }
        }
    }
}
