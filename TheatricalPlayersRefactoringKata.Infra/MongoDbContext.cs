using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace TheatricalPlayersRefactoringKata.Infra
{
    public class MongoContext
    {
        public MongoClient MongoClient { get; }
        private readonly IMongoDatabase database;

        public MongoContext(IConfiguration configuration)
        {
            var linkMongoClient = configuration.GetSection("MongoDB:ConnectionString").Value;
            var linkDatabase = configuration.GetSection("MongoDB:Database").Value;

            MongoClient = new MongoClient(linkMongoClient);
            database = MongoClient.GetDatabase(linkDatabase);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return database.GetCollection<T>(typeof(T).Name);
        }
    }
}