using System;

namespace ProjectTemplate.Framework.Commands
{
    public abstract class UpdateCommand : Command
    {
        protected UpdateCommand(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; init; }
    }
}
