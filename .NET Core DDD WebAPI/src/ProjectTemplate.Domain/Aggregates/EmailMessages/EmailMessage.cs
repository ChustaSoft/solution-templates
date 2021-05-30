using ProjectTemplate.Domain.Commands.EmailMessages;
using ProjectTemplate.Domain.Events.EmailMessages;
using ProjectTemplate.Domain.Shared.ValueTypes;
using ProjectTemplate.Framework.Aggregates;
using ProjectTemplate.Framework.Commands;
using ProjectTemplate.Framework.Events;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Domain.Aggregates.EmailMessages
{
    public class EmailMessage : Aggregate,
        ICommandHandler<CreateEmailMessageCommand>, IEventHandler<EmailMessageCreatedEvent>
    {
        public EmailMessage(IBus bus) : base(bus)
        {
        }

        public SubjectLine SubjectLine { get; private set; }
        public TextContent TextContent { get; private set; }
        public Email Destination { get; private set; }
        public Email Sender { get; private set; }

        public async Task ProcessCommand(CreateEmailMessageCommand command)
        {
            // Create an event that contains all information to produce the state change requested by the command. This
            // enables the current state of the aggregate to be reconstructed from all events that happened to it.
            var emailMessageCreatedEvent = new EmailMessageCreatedEvent
            {
                AggregateId = Guid.NewGuid(),
                AggregateType = GetType(),
                Destination = command.Destination,
                Sender = command.Sender,
                SubjectLine = command.SubjectLine,
                TextContent = command.TextContent
            };

            // Call the following function to
            // 1: change the state of this aggregate
            // 2: verify the state of the aggregate (i.e. running the invariants)
            // 3: raise the event in the system (which stores the event in the eventstore, triggers policies, updates projections)
            await ApplyAndRaiseEventOnSuccessAsync(emailMessageCreatedEvent);
        }

        public void PlayEvent(EmailMessageCreatedEvent domainEvent)
        {
            Id = domainEvent.AggregateId;
            Destination = domainEvent.Destination;
            SubjectLine = domainEvent.SubjectLine;
            TextContent = domainEvent.TextContent;
            Destination = domainEvent.Destination;
        }
    }
}
