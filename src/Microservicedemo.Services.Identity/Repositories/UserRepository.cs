using System;
using System.Threading.Tasks;
using Microservicedemo.Services.Identity.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Microservicedemo.Services.Identity.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _mongoDatabase;
        public UserRepository(IMongoDatabase database)
        {
            _mongoDatabase = database;
        }

        public async Task AddUser(User user)=>
            await Collection.InsertOneAsync(user);

        public async Task<User> GetAsync(Guid id)=>
            await Collection.AsQueryable().FirstOrDefaultAsync(x=> x.Id.Equals(id));

        public async Task<User> GetAsync(string email)=>
            await Collection
                    .AsQueryable().
                    FirstOrDefaultAsync(x=>x.Email.ToLower() == email.ToLower());


        private IMongoCollection<User> Collection =>
            _mongoDatabase.GetCollection<User>("Users");
    }
}