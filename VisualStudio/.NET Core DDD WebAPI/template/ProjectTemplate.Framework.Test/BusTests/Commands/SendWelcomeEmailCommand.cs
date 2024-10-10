using $ext_safeprojectname$.Framework.Commands;
using System;

namespace $ext_safeprojectname$.Framework.Test.BusTests.Commands
{
    public class SendWelcomeEmailCommand : UpdateCommand
    {
        public SendWelcomeEmailCommand(Guid userId) : base(userId)
        {
        }
    }
}
