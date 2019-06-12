using System.Collections.Generic;
using System.Threading.Tasks;
using Microservicedemo.Common.MongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Microservicedemo.Common.MongoDB
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly bool _seed;

        private readonly IMongoDatabase _database;

        private readonly IDatabaseSeeder _seeder ;

        public MongoInitializer(IMongoDatabase database,
         IDatabaseSeeder seeder,
         IOptions<MongoOptions> options)
        {
            _database = database;
            _seeder = seeder;
            _seed = options.Value.Seed;
        }

        public async Task InitializedAsync()
        {
            if(_initialized){
                return;
            }
            RegisterConventions();
            _initialized = true;
            if(!_seed)
            {
                return;
            }
            await _seeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("Action Conventions", new MongoConvention(), x=> true);
        }

        public class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}