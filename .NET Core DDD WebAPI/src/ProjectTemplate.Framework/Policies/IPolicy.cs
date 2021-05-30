using ProjectTemplate.Framework.Events;
using System.Threading.Tasks;

namespace ProjectTemplate.Framework.Policies
{
    public interface IPolicy<E> where E : DomainEvent
    {
        Task TriggerCommand(E domainEvent);
    }
}
