using ProjectTemplate.Framework.Policies;
using ProjectTemplate.Framework.Test.BusTests.Commands;
using ProjectTemplate.Framework.Test.BusTests.Events;
using System.Threading.Tasks;

namespace ProjectTemplate.Framework.Test.BusTests.Policies
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
