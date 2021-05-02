using $ext_safeprojectname$.Framework.Commands;
using System;

namespace $ext_safeprojectname$.Framework.Test.BusTests.Commands
{
    public class UpdateUserCommand : UpdateCommand
    {
        public UpdateUserCommand(Guid userId) : base(userId)
        {
        }

        public string NewName { get; init; }
    }
}
