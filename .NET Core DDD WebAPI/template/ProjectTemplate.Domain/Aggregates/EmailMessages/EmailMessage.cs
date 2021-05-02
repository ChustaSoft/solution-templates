using $ext_safeprojectname$.Domain.Aggregates.Users;
using $ext_safeprojectname$.Domain.Commands.EmailMessages;
using $ext_safeprojectname$.Domain.Events.EmailMessages;
using $ext_safeprojectname$.Domain.Shared.ValueTypes;
using $ext_safeprojectname$.Framework.Aggregates;
using $ext_safeprojectname$.Framework.Commands;
using $ext_safeprojectname$.Framework.Events;
using System;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Domain.Aggregates.EmailMessages
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
            var emailMessageCreatedEvent = new EmailMessageCreatedEvent
            {
                AggregateId = Guid.NewGuid(),
                AggregateType = GetType(),
                Destination = command.Destination,
                Sender = command.Sender,
                SubjectLine = command.SubjectLine,
                TextContent = command.TextContent
            };

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
