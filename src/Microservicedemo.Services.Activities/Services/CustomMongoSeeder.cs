using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservicedemo.Common.MongoDB;
using Microservicedemo.Services.Activities.Domain.Models;
using Microservicedemo.Services.Activities.Domain.Repository;
using MongoDB.Driver;

namespace Microservicedemo.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRespository;
        public CustomMongoSeeder(IMongoDatabase database, ICategoryRepository categoryRepo) : base(database)
        {
            _categoryRespository = categoryRepo;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>{
                "work",
                "sport",
                "hobby"
            };
            await Task.WhenAll(categories.Select(x=>_categoryRespository.AddAsync(new Category(x))));
        }
    }
}