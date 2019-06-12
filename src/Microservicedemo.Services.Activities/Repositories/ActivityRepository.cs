using System;
using System.Threading.Tasks;
using Microservicedemo.Services.Activities.Domain.Models;
using Microservicedemo.Services.Activities.Domain.Repository;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Microservicedemo.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {

        private readonly IMongoDatabase _mongoDatabase;
        public ActivityRepository(IMongoDatabase database)
        {
            _mongoDatabase = database;
        }

        public async Task AddAsync(Activity activity)
            => await Collection.InsertOneAsync(activity);

        public async Task<Activity> GetAsync(Guid id)
            => await Collection.AsQueryable().FirstOrDefaultAsync(x=>x.Id.Equals(id));

        private IMongoCollection<Activity> Collection
            => _mongoDatabase.GetCollection<Activity>("Activities");
    }
}