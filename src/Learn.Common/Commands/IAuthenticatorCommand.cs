using System;

namespace Learn.Common.Commands
{
    public interface IAuthenticatorCommand : ICommand
    {
         Guid UserId{get;set;}
    }
}