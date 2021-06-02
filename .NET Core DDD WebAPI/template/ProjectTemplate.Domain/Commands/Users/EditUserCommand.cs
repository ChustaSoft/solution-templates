using $ext_safeprojectname$.Domain.Aggregates.Users;
using $ext_safeprojectname$.Domain.Shared.ValueTypes;
using $ext_safeprojectname$.Framework.Commands;
using System;

namespace $ext_safeprojectname$.Domain.Commands.Users
{
    public class EditUserCommand : UpdateCommand
    {
        public EditUserCommand(Guid aggregateId, string firstName, string lastName, string recoveryEmail) : base(aggregateId)
        {
            FirstName = firstName;
            LastName = lastName;
            RecoveryEmail = recoveryEmail;
        }

        public FirstName FirstName { get; }
        public LastName LastName { get; }
        public Email RecoveryEmail { get; }
    }
}
