using System;

namespace $ext_safeprojectname$.Framework.Commands
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
