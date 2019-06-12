using System.Collections.Generic;
using System.Threading.Tasks;
using Microservicedemo.Services.Activities.Domain.Models;

namespace Microservicedemo.Services.Activities.Domain.Repository
{
    public interface ICategoryRepository
    {
         Task<Category> GetAsync(string name);

         Task<IEnumerable<Category>> BrowseAsync();
         
         Task AddAsync(Category category);
    }
}