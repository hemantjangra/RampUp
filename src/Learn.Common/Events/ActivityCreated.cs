using System;

namespace Learn.Common.Events
{
    public class ActivityCreated : IAuthenticatorEvent
    {
        public Guid Id{get;}
        public Guid UserId { get;}

        public string Category{get;}
        public string Name{get;}
        public string Description{get;}

        public DateTime CreatedAt { get;  }

        protected ActivityCreated()
        {
            
        }

        public ActivityCreated(Guid id, Guid userid, string category, string name)
        {
            this.Id = id;
            this.UserId = userid;
            this.Category = category;
            this.Name = name;
        }
    }
}