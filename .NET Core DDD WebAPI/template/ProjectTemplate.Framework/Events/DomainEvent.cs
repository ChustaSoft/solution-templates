using System;

namespace $ext_safeprojectname$.Framework.Events
{
    public abstract class DomainEvent
    {
        public Guid AggregateId { get; init; }
        public Type AggregateType { get; init; }
    }
}
