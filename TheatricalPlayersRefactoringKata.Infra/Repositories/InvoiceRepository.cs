using MongoDB.Bson;
using MongoDB.Driver;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interface;

namespace TheatricalPlayersRefactoringKata.Infra.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly MongoContext _mongoContext;

        public InvoiceRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<bool> Create(Invoice invoice)
        {
            try
            {
                await _mongoContext.GetCollection<Invoice>().InsertOneAsync(invoice);

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

                var filterId = Builders<Invoice>.Filter.Eq("_id", objId);

                await _mongoContext.GetCollection<Invoice>().DeleteOneAsync(filterId);

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Invoice>> GetAllByCustomer(string customerName)
        {
            try
            {
                var filterId = Builders<Invoice>.Filter.Eq(x => x.Customer, customerName);

                var result = await _mongoContext.GetCollection<Invoice>().Find(filterId).ToListAsync();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Invoice>> GetAllByPlay(string playId)
        {
            try
            {
                var filterId = Builders<Invoice>.Filter.Eq("Performances.PlayId", playId);

                var result = await _mongoContext.GetCollection<Invoice>().Find(filterId).ToListAsync();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
