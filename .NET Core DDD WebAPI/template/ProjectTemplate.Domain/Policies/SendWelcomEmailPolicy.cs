using $ext_safeprojectname$.Domain.Commands.EmailMessages;
using $ext_safeprojectname$.Domain.Events.Users;
using $ext_safeprojectname$.Framework.Policies;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Domain.Policies
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
                subjectLine: "Welcome to $ext_safeprojectname$",
                textContent: $"Dear {domainEvent.FirstName}: Welcome to $ext_safeprojectname$!",
                destination: domainEvent.Email
            );
            return Task.FromResult(command);
        }
    }
}
