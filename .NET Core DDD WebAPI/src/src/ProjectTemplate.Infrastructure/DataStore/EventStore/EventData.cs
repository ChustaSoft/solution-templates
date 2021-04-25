using System;

namespace ProjectTemplate.Infrastructure.DataStore.EventStore
{
    public class EventData
    {
        public int Id { get; init; }
        public Guid AggregateId { get; init; }
        public string AggregateType { get; init; }
        public string EventType { get; set; }
        public string Content { get; init; }
    }
}
