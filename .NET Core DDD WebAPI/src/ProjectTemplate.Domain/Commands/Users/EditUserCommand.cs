using ProjectTemplate.Domain.Aggregates.Users;
using ProjectTemplate.Domain.Shared.ValueTypes;
using ProjectTemplate.Framework.Commands;
using System;

namespace ProjectTemplate.Domain.Commands.Users
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
