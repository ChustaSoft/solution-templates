using ProjectTemplate.Framework.Commands;
using System;

namespace ProjectTemplate.Framework.Test.BusTests.Commands
{
    public class UpdateUserCommand : UpdateCommand
    {
        public UpdateUserCommand(Guid userId) : base(userId)
        {
        }

        public string NewName { get; init; }
    }
}
