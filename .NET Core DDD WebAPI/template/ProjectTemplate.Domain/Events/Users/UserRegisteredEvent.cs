using $ext_safeprojectname$.Framework.Events;

namespace $ext_safeprojectname$.Domain.Events.Users
{
    public class UserRegisteredEvent : DomainEvent
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string RecoveryEmail { get; init; }
    }
}
