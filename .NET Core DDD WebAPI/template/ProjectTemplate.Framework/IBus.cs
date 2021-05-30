using $ext_safeprojectname$.Framework.Commands;
using $ext_safeprojectname$.Framework.Events;
using System.Threading.Tasks;

namespace $ext_safeprojectname$
{
    public interface IBus
    {
        Task ProcessCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T domainEvent) where T : DomainEvent;
    }
}
