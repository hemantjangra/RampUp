using System.Threading.Tasks;
using Microservicedemo.Services.Activities.Domain.Models;
using System;

namespace Microservicedemo.Services.Activities.Domain.Repository
{
    public interface IActivityRepository
    {
         Task<Activity> GetAsync(Guid id);
         Task AddAsync(Activity activity);
    }
}