using System;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Microservicedemo.Common.Authentication
{
    public class JsonWebToken
    {
        public string Token { get; set; }

        public long Expires { get; set; }
    }
}