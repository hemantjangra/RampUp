using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using microservicedemo.API.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace microservicedemo.API.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _mongoDatabase;
        public ActivityRepository(IMongoDatabase database)
        {
            _mongoDatabase = database;
        }

        public async Task AddAsync(Activity activity)
        {
            await Collection.InsertOneAsync(activity);
        }

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
        {
            return await Collection.AsQueryable().Where(x=>x.UserId  == userId).ToListAsync();
        }

        public async Task<Activity> GetAsync(Guid id)
        {
            return await Collection.AsQueryable().FirstOrDefaultAsync(x=>x.Id == id);
        }

        private IMongoCollection<Activity> Collection =>
            _mongoDatabase.GetCollection<Activity>("Activities");
    }
}