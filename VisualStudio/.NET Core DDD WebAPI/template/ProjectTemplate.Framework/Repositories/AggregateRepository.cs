using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using $ext_safeprojectname$.Framework.Aggregates;
using $ext_safeprojectname$.Framework.Events;

namespace $ext_safeprojectname$.Framework.AggregateRepositories
{
    public class AggregateRepository<T> : IAggregateRepository<T> where T : Aggregate 
    {
        private readonly IEventStore eventStore;
        private readonly IServiceProvider serviceProvider;

        public AggregateRepository(IEventStore eventStore, IServiceProvider serviceProvider)
        {
            this.eventStore = eventStore;
            this.serviceProvider = serviceProvider;
        }

        public async Task<T> GetById(Guid id)
        {
            var events = await eventStore.GetEventStream(id);

            if (events.Count == 0)
            {
                return null;
            }
            else
            {
                return ReconstructCurrentState(events);
            }
        }

        public async Task<List<T>> GetAll()
        {
            var events = await eventStore.GetEventsForAggregates<T>();

            return events
                .GroupBy(e => e.AggregateId)
                .Select(ReconstructCurrentState)
                .ToList();

        }

        private T ReconstructCurrentState(IEnumerable<DomainEvent> domainEvents)
        {
            var aggregate = serviceProvider.GetService<T>();
            aggregate.ConstructCurrentState(domainEvents);
            return aggregate;
        }
    }
}
