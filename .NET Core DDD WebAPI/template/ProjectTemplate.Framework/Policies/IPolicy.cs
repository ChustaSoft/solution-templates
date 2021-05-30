using $ext_safeprojectname$.Framework.Events;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Framework.Policies
{
    public interface IPolicy<E> where E : DomainEvent
    {
        Task TriggerCommand(E domainEvent);
    }
}
