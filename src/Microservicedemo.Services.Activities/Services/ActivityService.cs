using System;
using System.Threading.Tasks;
using Microservicedemo.Common.Exceptions;
using Microservicedemo.Services.Activities.Domain.Models;
using Microservicedemo.Services.Activities.Domain.Repository;

namespace Microservicedemo.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {

        private readonly ICategoryRepository _categoryReposistory;

        private readonly IActivityRepository _activityRepository;

        public ActivityService(ICategoryRepository categoryRepo, IActivityRepository activityRepo)
        {
            _categoryReposistory = categoryRepo;
            _activityRepository = activityRepo;
        }
        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activityCategory = await _categoryReposistory.GetAsync(name);
            if(activityCategory==null){
                throw new LearnException("Category Not Found", $"Category: {category} was not found");
            }
            var activity = new Activity(id, name, category, description, userId, createdAt);
            await _activityRepository.AddAsync(activity);
        }
    }
}