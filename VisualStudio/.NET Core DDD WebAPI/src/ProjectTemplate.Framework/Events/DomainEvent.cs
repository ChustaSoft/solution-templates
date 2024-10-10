using System;

namespace ProjectTemplate.Framework.Events
{
    public abstract class DomainEvent
    {
        public Guid AggregateId { get; init; }
        public Type AggregateType { get; init; }
    }
}
