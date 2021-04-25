using ProjectTemplate.Domain.Commands.Users;
using ProjectTemplate.Domain.DomainServices.Repositories.Users;
using ProjectTemplate.Domain.Events.Users;
using ProjectTemplate.Domain.Shared.ValueTypes;
using ProjectTemplate.Framework;
using ProjectTemplate.Framework.Aggregates;
using ProjectTemplate.Framework.Commands;
using ProjectTemplate.Framework.Events;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Domain.Aggregates.Users
{
    public class User : Aggregate,
        ICommandHandler<RegisterUserCommand>, IEventHandler<UserRegisteredEvent>,
        ICommandHandler<EditUserCommand>, IEventHandler<UserEditedEvent>
    {
        private readonly IUserRepository userRepository;

        public User(IBus bus, IUserRepository userRepository) 
            : base(bus)
        {
            this.userRepository = userRepository;
        }


        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public Email RecoveryEmail { get; private set; }

        public async Task ProcessCommand(RegisterUserCommand command)
        {
            var UserWithEmailExists = await userRepository.UserExists(command.Email);
            if (UserWithEmailExists) throw new InvalidOperationException("trying to add a user that already exists");

            var userRegisteredEvent = new UserRegisteredEvent
            {
                AggregateId = Guid.NewGuid(),
                AggregateType = GetType(),
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                RecoveryEmail = command.RecoveryEmail
            };
            await ApplyAndRaiseEventOnSuccessAsync(userRegisteredEvent);
        }

        public void PlayEvent(UserRegisteredEvent domainEvent)
        {
            FirstName = domainEvent.FirstName;
            LastName = domainEvent.LastName;
            Email = domainEvent.Email;
            RecoveryEmail = domainEvent.RecoveryEmail;
        }        

        public void PlayEvent(UserEditedEvent domainEvent)
        {
            FirstName = domainEvent.FirstName;
            LastName = domainEvent.LastName;
            RecoveryEmail = domainEvent.RecoveryEmail;
        }

        public async Task ProcessCommand(EditUserCommand command)
        {
            var userEditedEvent = new UserEditedEvent
            {
                AggregateId = Id,
                AggregateType = GetType(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                RecoveryEmail = command.RecoveryEmail
            };
            await ApplyAndRaiseEventOnSuccessAsync(userEditedEvent);
        }

        [Invariant]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "This is discovered by reflection")]
        private void RecoveryEmail_must_differ_from_PrimaryEmail()
        {
            if (Email == RecoveryEmail)
                throw new InvalidOperationException("Email address for account recovery must be different from primary email");
        }
    }
}
