using System.Threading.Tasks;
using microservicedemo.API.Models;
using System;
using System.Collections.Generic;

namespace microservicedemo.API.Repositories
{
    public interface IActivityRepository
    {
         Task AddAsync(Activity activity);

         Task<Activity> GetAsync(Guid id);

         Task<IEnumerable<Activity>> BrowseAsync(Guid userId);
    }
}