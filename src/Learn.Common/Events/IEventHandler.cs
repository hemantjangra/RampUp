using System.Threading.Tasks;

namespace Learn.Common.Events
{
    public interface IEventHandler<in T> where T: IEvent
    {
         Task HandleAsync(T Event);
    }
}