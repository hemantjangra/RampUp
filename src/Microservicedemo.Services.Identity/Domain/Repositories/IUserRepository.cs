using System.Threading.Tasks;
using Microservicedemo.Services.Identity.Domain.Models;
using System;

namespace Microservicedemo.Services.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);

        Task<User> GetAsync(string email);

        Task AddUser(User user);
    }
}