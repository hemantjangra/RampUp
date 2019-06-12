using System;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Microservicedemo.Common.Authentication
{
    public interface IJWTHandler
    {
         JsonWebToken Create(Guid userId);
    }
}