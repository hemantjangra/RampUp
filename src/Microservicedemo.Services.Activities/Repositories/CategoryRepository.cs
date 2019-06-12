using System.Collections.Generic;
using System.Threading.Tasks;
using Microservicedemo.Services.Activities.Domain.Models;
using Microservicedemo.Services.Activities.Domain.Repository;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Microservicedemo.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase _mongoDatabase;
        public CategoryRepository(IMongoDatabase database)
        {
            _mongoDatabase = database;
        }

        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);

        public async Task<IEnumerable<Category>> BrowseAsync()
            => await Collection.AsQueryable().ToListAsync();

        public async Task<Category> GetAsync(string name)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Name == name.ToLowerInvariant());

        private IMongoCollection<Category> Collection
            => _mongoDatabase.GetCollection<Category>("Categories");
    }
}