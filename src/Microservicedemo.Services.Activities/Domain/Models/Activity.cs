using System;
using Microservicedemo.Common.Exceptions;

namespace Microservicedemo.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id{get; protected set;}
        
        public string Name{get; protected set;}

        public string Category{get; protected set;}

        public string Description { get; protected set; }

        public Guid UserId{get; protected set;}

        public DateTime CreatedAt{get; protected set;}

        protected Activity()
        {
        
        }

        public Activity(Guid id, string name, string category, string description, Guid userId, DateTime createdAt)
        {
            if(string.IsNullOrEmpty(name)){
                throw new LearnException("activity_name_empty", "Activity Name can not be Empty");
            }
            Id = id;
            Name = name;
            Category = category;
            Description = description;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }

    
}