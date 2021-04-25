using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProjectTemplate.Framework.Events;

namespace ProjectTemplate.Framework.Projections
{
    public class ReadModelRegenerator : IReadModelRegenerator
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IEventStore eventStore;

        public ReadModelRegenerator(IServiceProvider serviceProvider, IEventStore eventStore)
        {
            this.serviceProvider = serviceProvider;
            this.eventStore = eventStore;
        }

        public async Task RegenerateReadModel()
        {
            var domainEvents = await eventStore.GetEventStream();

            foreach (var domainEvent in domainEvents)
            {
                await ProcessEvent(domainEvent);
            }
        }

        private async Task ProcessEvent(DomainEvent domainEvent)
        {
            var processEventTask = GetType()
                .GetMethod(nameof(ProcessEventGeneric), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(domainEvent.GetType())
                .Invoke(this, new[] { domainEvent })
                as Task;

            await processEventTask;
        }

        private async Task ProcessEventGeneric<T>(T domainEvent) where T : DomainEvent
        {
            var projections = serviceProvider.GetServices<IProjection<T>>();

            foreach (var projection in projections)
            {
                await projection.Project(domainEvent);
            }
        }
    }
}
