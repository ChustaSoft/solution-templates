using ProjectTemplate.Domain.Commands.EmailMessages;
using ProjectTemplate.Domain.Events.Users;
using ProjectTemplate.Framework.Policies;
using System.Threading.Tasks;

namespace ProjectTemplate.Domain.Policies
{
    public class SendWelcomEmailPolicy : Policy<UserRegisteredEvent, CreateEmailMessageCommand>
    {
        public SendWelcomEmailPolicy(IBus bus) : base(bus)
        {
        }

        public override Task<CreateEmailMessageCommand> CreateCommand(UserRegisteredEvent domainEvent)
        {
            var command = new CreateEmailMessageCommand(
                sender: "noreply@domain.com",
                subjectLine: "Welcome to $safeprojectname$",
                textContent: $"Dear {domainEvent.FirstName}: Welcome to $safeprojectname$!",
                destination: domainEvent.Email
            );
            return Task.FromResult(command);
        }
    }
}
