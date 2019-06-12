namespace Microservicedemo.Common.Authentication
{
    public class JWTOptions
    {
        public string SecretKey { get; set; }

        public int ExpirationMinutes { get; set; }

        public string Issuer { get; set; }
    }
}