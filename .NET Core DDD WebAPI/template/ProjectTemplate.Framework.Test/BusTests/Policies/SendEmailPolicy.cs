using $ext_safeprojectname$.Framework.Policies;
using $ext_safeprojectname$.Framework.Test.BusTests.Commands;
using $ext_safeprojectname$.Framework.Test.BusTests.Events;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Framework.Test.BusTests.Policies
{
    public class SendEmailPolicy : Policy<UserCreatedEvent, SendWelcomeEmailCommand>
    {
        public SendEmailPolicy(IBus bus) : base(bus)
        {
        }

        public override Task<SendWelcomeEmailCommand> CreateCommand(UserCreatedEvent domainEvent) {
            var command = new SendWelcomeEmailCommand(domainEvent.AggregateId);
            return Task.FromResult(command);
        }
    }
}
