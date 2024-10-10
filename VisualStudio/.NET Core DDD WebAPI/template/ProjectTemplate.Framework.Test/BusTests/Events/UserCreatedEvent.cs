using $ext_safeprojectname$.Framework.Events;

namespace $ext_safeprojectname$.Framework.Test.BusTests.Events
{
    public class UserCreatedEvent : DomainEvent
    {
        public string Name { get; init; }
    }
}
