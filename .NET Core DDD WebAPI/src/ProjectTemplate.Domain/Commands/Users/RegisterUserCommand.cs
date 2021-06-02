using ProjectTemplate.Domain.Aggregates.Users;
using ProjectTemplate.Domain.Shared.ValueTypes;
using ProjectTemplate.Framework.Commands;

namespace ProjectTemplate.Domain.Commands.Users
{
    public class RegisterUserCommand : CreationCommand
    {
        public RegisterUserCommand(string firstName, string lastName, string email, string recoveryEmail)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RecoveryEmail = recoveryEmail;
        }

        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public Email Email { get; }
        public Email RecoveryEmail { get; }
    }
}
