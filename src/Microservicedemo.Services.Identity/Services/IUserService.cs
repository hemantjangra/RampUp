using System.Threading.Tasks;
using Microservicedemo.Common.Authentication;

namespace Microservicedemo.Services.Identity.Services
{
    public interface IUserService
    {
         Task RegisterAsync(string email, string password, string name);

         Task<JsonWebToken> LoginAsync(string email, string password);
    }
}