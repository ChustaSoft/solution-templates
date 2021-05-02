using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using $ext_safeprojectname$.Framework.Events;

namespace $ext_safeprojectname$.Infrastructure.DataStore.EventStore
{
    public class TSqlEventStore : IEventStore
    {
        private readonly Assembly domainAssembly;
        private readonly string _dbConnectionString;

        public TSqlEventStore(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
            domainAssembly = typeof(Domain.StartupExtensions).Assembly;
            using var context = new Context(_dbConnectionString);
            context.Database.EnsureCreated();
        }

        public async Task<List<DomainEvent>> GetEventStream(Guid aggregateId)
        {
            using var context = new Context(_dbConnectionString);

            var eventStream = await context.EventData
                .Where(ed => ed.AggregateId == aggregateId)
                .ToListAsync();

            return eventStream
                .Select(ToDomainEvent)
                .ToList();
        }

        public async Task<List<DomainEvent>> GetEventStream()
        {
            using var context = new Context(_dbConnectionString);

            var eventStream = await context.EventData.ToListAsync();

            return eventStream
                .Select(ToDomainEvent)
                .ToList();
        }

        public async Task StoreEvent(DomainEvent domainEvent)
        {
            var eventData = ToEventData(domainEvent);

            using var context = new Context(_dbConnectionString);
            context.EventData.Add(eventData);
            await context.SaveChangesAsync();
        }

        private DomainEvent ToDomainEvent(EventData eventData)
        {
            var eventType = domainAssembly.GetType(eventData.EventType);
            return (DomainEvent) JsonConvert.DeserializeObject(eventData.Content, eventType);
        }

        private static EventData ToEventData(DomainEvent domainEvent)
        {
            return new EventData
            {
                AggregateId = domainEvent.AggregateId,
                AggregateType = domainEvent.AggregateType.FullName,
                EventType = domainEvent.GetType().FullName,
                Content = JsonConvert.SerializeObject(domainEvent)
            };
        }

        public async Task<List<DomainEvent>> GetEventsForAggregates<T>()
        {
            using var context = new Context(_dbConnectionString);

            var eventStream = await context.EventData
                .Where(ed => ed.AggregateType == typeof(T).FullName)
                .ToListAsync();

            return eventStream
                .Select(ToDomainEvent)
                .ToList();
        }
    }
}
