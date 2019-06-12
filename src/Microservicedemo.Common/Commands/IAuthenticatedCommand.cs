using System;

namespace Microservicedemo.Common.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
         Guid UserId{get;set;}
    }
}