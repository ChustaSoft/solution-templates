using ProjectTemplate.Framework.Commands;
using System;

namespace ProjectTemplate.Framework.Test.BusTests.Commands
{
    public class SendWelcomeEmailCommand : UpdateCommand
    {
        public SendWelcomeEmailCommand(Guid userId) : base(userId)
        {
        }
    }
}
