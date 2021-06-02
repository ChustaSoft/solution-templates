using ProjectTemplate.Framework.Events;

namespace ProjectTemplate.Framework.Test.BusTests.Events
{
    public class UserUpdatedEvent : DomainEvent
    {
        public string Name { get; set; }
    }
}
