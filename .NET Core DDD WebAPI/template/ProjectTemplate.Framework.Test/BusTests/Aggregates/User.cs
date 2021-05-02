using $ext_safeprojectname$.Framework.Aggregates;
using $ext_safeprojectname$.Framework.Commands;
using $ext_safeprojectname$.Framework.Events;
using $ext_safeprojectname$.Framework.Test.BusTests.Commands;
using $ext_safeprojectname$.Framework.Test.BusTests.Events;
using System;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Framework.Test.BusTests.Aggregates
{
    public class User : Aggregate,
        ICommandHandler<CreateUserCommand>, IEventHandler<UserCreatedEvent>,
        ICommandHandler<UpdateUserCommand>, IEventHandler<UserUpdatedEvent>,
        ICommandHandler<SendWelcomeEmailCommand>, IEventHandler<WelcomeEmailSentEvent>,
        ICommandHandler<CommandThatTriggersUnhandledEvent>
    {
        public User(IBus bus) : base(bus)
        {
        }

        public async Task ProcessCommand(CreateUserCommand command)
        {
            var userCreatedEvent = new UserCreatedEvent {
                AggregateId = Guid.NewGuid(),
                AggregateType = typeof(User),
                Name = command.Name };

            await ApplyAndRaiseEventOnSuccessAsync(userCreatedEvent);
        }

        public void PlayEvent(UserCreatedEvent domainEvent)
        {
            Name = domainEvent.Name;
            Id = domainEvent.AggregateId;
        }

        public async Task ProcessCommand(CommandThatTriggersUnhandledEvent command)
        {
            var eventWithoutHandler = new EventWithoutHandler();
            await ApplyAndRaiseEventOnSuccessAsync(eventWithoutHandler);
        }

        public async Task ProcessCommand(UpdateUserCommand command)
        {
            var userUpdatedEvent = new UserUpdatedEvent {
                AggregateId = command.AggregateId,
                AggregateType = typeof(User),
                Name = command.NewName
            };

            await ApplyAndRaiseEventOnSuccessAsync(userUpdatedEvent);

        }

        public void PlayEvent(UserUpdatedEvent domainEvent)
        {
            Name = domainEvent.Name;
        }

        public async Task ProcessCommand(SendWelcomeEmailCommand command)
        {
            await ApplyAndRaiseEventOnSuccessAsync(new WelcomeEmailSentEvent());
        }

        public void PlayEvent(WelcomeEmailSentEvent domainEvent)
        {
        }

        public string Name { get; private set; }


        [Invariant]
        private void EnsureNameIsValid()
        {
            if (Name == "Jeves")
                throw new InvalidOperationException(@"You misspelled ""Jeeves""");
        }
    }
}
