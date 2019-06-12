using System.Threading.Tasks;

namespace Microservicedemo.Common.MongoDB
{
    public interface IDatabaseInitializer
    {
         Task InitializedAsync();
    }
}