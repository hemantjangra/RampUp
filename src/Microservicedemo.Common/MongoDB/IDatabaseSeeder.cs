using System.Threading.Tasks;

namespace Microservicedemo.Common.MongoDB
{
    public interface IDatabaseSeeder
    {
         Task SeedAsync();
    }
}