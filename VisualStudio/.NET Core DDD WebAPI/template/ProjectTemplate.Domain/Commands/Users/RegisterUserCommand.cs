using $ext_safeprojectname$.Domain.Aggregates.Users;
using $ext_safeprojectname$.Domain.Shared.ValueTypes;
using $ext_safeprojectname$.Framework.Commands;

namespace $ext_safeprojectname$.Domain.Commands.Users
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
