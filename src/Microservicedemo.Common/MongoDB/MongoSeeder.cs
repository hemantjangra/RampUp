using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Microservicedemo.Common.MongoDB
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase _mongoDatabase;

        public MongoSeeder(IMongoDatabase database)
        {
            _mongoDatabase = database;
        }
        public async Task SeedAsync()
        {
            var collectionCursor = await _mongoDatabase.ListCollectionsAsync();
            var collections = await collectionCursor.ToListAsync();
            if(collections.Any())
            {
                return;
            }
            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}