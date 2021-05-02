using $ext_safeprojectname$.Framework.Commands;
using $ext_safeprojectname$.Framework.Events;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Framework.Policies
{
    public abstract class Policy<E, C> : IPolicy<E> where E : DomainEvent where C : Command
    {
        private readonly IBus bus;

        public Policy(IBus bus)
        {
            this.bus = bus;
        }

        public abstract Task<C> CreateCommand(E domainEvent);

        public async Task TriggerCommand(E domainEvent)
        {
            var triggeredCommand = await CreateCommand(domainEvent);
            await bus.ProcessCommand(triggeredCommand);
        }
    }
}
