using System;

namespace Learn.Common.Events
{
    public interface IAuthenticatorEvent : IEvent
    {
         Guid UserId{get;}
    }
}