using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Framework.Events
{
    public interface IEventStore
    {
        Task StoreEvent(DomainEvent domainEvent);
        Task<List<DomainEvent>> GetEventStream(Guid aggregateId);
        Task<List<DomainEvent>> GetEventStream();
        Task<List<DomainEvent>> GetEventsForAggregates<T>();
    }
}
