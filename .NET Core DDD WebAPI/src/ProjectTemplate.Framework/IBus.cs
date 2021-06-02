using ProjectTemplate.Framework.Commands;
using ProjectTemplate.Framework.Events;
using System.Threading.Tasks;

namespace ProjectTemplate
{
    public interface IBus
    {
        Task ProcessCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T domainEvent) where T : DomainEvent;
    }
}
