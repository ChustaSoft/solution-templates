using $ext_safeprojectname$.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Framework.Aggregates
{
    public abstract class Aggregate
    {

        private readonly string eventHandlerName;
        private readonly IBus bus;


        public Guid Id { get; protected set; }


        protected Aggregate(IBus bus)
        {
            this.bus = bus;
            eventHandlerName = typeof(IEventHandler<>).Name; // this string is cached because it is often used
        }


        public async Task ApplyAndRaiseEventOnSuccessAsync<T>(T domainEvent) where T : DomainEvent
        {
            Apply(domainEvent);
            RunInvariants();
            await bus.RaiseEvent(domainEvent);
        }

        public void ConstructCurrentState(IEnumerable<DomainEvent> domainEvents)
        {
            Id = domainEvents.First().AggregateId;
            foreach (var domainEvent in domainEvents)
            {
                Apply(domainEvent);
            }
        }


        private void Apply(DomainEvent domainEvent)
        {
            var eventHandler = GetType()
                .GetInterfaces()
                .Where(i => i.Name == eventHandlerName)
                .Where(i => i.GenericTypeArguments.First() == domainEvent.GetType())
                .FirstOrDefault()
                ?? throw new InvalidOperationException($@"Please make sure that the aggregate ""{GetType().Name}"" implements ""{nameof(IEventHandler<DomainEvent>)}<{domainEvent.GetType().Name}>""");

            var playEventMethod = eventHandler
                .GetMethods()
                .First(m => m.GetParameters().Length == 1 && m.GetParameters().First().ParameterType == domainEvent.GetType());

            playEventMethod.Invoke(this, new[] { domainEvent });
        }

        private void RunInvariants()
        {
            GetType()
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(p => Attribute.IsDefined(p, typeof(InvariantAttribute)))
                .ToList()
                .ForEach(RunInvariant);
        }

        private void RunInvariant(MethodInfo invariantMethod)
        {
            try
            {
                invariantMethod.Invoke(this, null);
            }
            catch (TargetInvocationException targetInvocationException)
            {
                throw targetInvocationException.InnerException;
            }
        }

    }
}
