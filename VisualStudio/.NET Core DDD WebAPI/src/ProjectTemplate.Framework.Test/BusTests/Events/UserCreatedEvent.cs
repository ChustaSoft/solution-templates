using ProjectTemplate.Framework.Events;

namespace ProjectTemplate.Framework.Test.BusTests.Events
{
    public class UserCreatedEvent : DomainEvent
    {
        public string Name { get; init; }
    }
}
