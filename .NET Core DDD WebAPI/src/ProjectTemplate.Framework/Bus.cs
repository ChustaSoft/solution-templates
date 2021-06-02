using ProjectTemplate.Framework.Commands;
using ProjectTemplate.Framework.Policies;
using ProjectTemplate.Framework.Projections;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using ProjectTemplate.Framework.Aggregates;
using ProjectTemplate.Framework.Events;

namespace ProjectTemplate.Framework
{
    public class Bus : IBus
    {
        private readonly IEventStore eventStore;
        private readonly IServiceProvider serviceProvider;

        public Bus(IEventStore eventStore, IServiceProvider serviceProvider)
        {
            this.eventStore = eventStore;
            this.serviceProvider = serviceProvider;
        }

        public async Task ProcessCommand<T>(T command) where T : Command
        {
            var commandHandler = GetCommandHandler<T>();

            switch (command)
            {
                case CreationCommand:

                    await commandHandler.ProcessCommand(command);
                    break;
                case UpdateCommand updateCommand:

                    var aggregate = (Aggregate)commandHandler;
                    var events = await eventStore.GetEventStream(updateCommand.AggregateId);
                    if (events.Count == 0) throw new InvalidOperationException($"no aggregate found with id {updateCommand.AggregateId}");
                    aggregate.ConstructCurrentState(events);

                    await commandHandler.ProcessCommand(command);
                    break;
            }
        }

        public async Task RaiseEvent<T>(T domainEvent) where T : DomainEvent
        {
            await eventStore.StoreEvent(domainEvent);
            await CalculateProjections(domainEvent);
            await TriggerPolicies(domainEvent);
        }

        private async Task CalculateProjections<T>(T domainEvent) where T : DomainEvent
        {
            var projections = serviceProvider.GetServices<IProjection<T>>();

            foreach (var projection in projections)
            {
                await projection.Project(domainEvent);
            }
        }

        private ICommandHandler<T> GetCommandHandler<T>() where T : Command
        {
            return serviceProvider.GetService<ICommandHandler<T>>()
                ?? throw new InvalidOperationException(@$"Please make sure that an aggregate that implements ""{nameof(ICommandHandler<T>)}<{typeof(T).Name}>"" is both implemented and registered in the DI container");
        }

        private async Task TriggerPolicies<T>(T domainEvent) where T : DomainEvent
        {
            var policies = serviceProvider.GetServices<IPolicy<T>>();

            foreach (var policy in policies)
            {
                await policy.TriggerCommand(domainEvent);
            }
        }
    }
}
