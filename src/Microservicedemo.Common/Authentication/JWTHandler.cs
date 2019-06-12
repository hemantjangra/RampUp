using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Microservicedemo.Common.Authentication
{
    public class JWTHandler : IJWTHandler
    {

        private readonly JWTOptions _options;

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        private readonly SecurityKey _issueSigningKey;

        private readonly SigningCredentials _signingCredentials;

        private readonly JwtHeader _jwtHeader;

        private readonly TokenValidationParameters _tokenValidationParameters;

        public JWTHandler(IOptions<JWTOptions> options)
        {
            Console.WriteLine("JWTHandler called");
            _options = options.Value;
            _issueSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issueSigningKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters{
                ValidateAudience = false,
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = _issueSigningKey
            };
        }

        public JsonWebToken Create(Guid userId)
        {
            var nowUTC = DateTime.UtcNow;
            var expires = nowUTC.AddMinutes(_options.ExpirationMinutes);
            var centuaryBegins = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks-centuaryBegins.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUTC.Ticks - centuaryBegins.Ticks).TotalSeconds);
            var payload = new JwtPayload
            {
                {"sub", userId},
                {"iss", _options.Issuer},
                {"iat", now},
                {"exp", exp},
                {"unique_name", userId}
            };
            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}