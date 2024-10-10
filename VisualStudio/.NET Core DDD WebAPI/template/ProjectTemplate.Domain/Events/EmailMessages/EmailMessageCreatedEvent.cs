using $ext_safeprojectname$.Framework.Events;

namespace $ext_safeprojectname$.Domain.Events.EmailMessages
{
    public class EmailMessageCreatedEvent : DomainEvent
    {
        public string Sender { get; init; }
        public string Destination { get; init; }
        public string SubjectLine { get; init; }
        public string TextContent { get; init; }
    }
}
