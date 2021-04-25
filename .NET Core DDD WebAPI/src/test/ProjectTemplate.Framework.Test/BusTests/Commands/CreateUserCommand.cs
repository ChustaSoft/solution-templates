using ProjectTemplate.Framework.Commands;

namespace ProjectTemplate.Framework.Test.BusTests.Commands
{
    public class CreateUserCommand : CreationCommand
    {
        public string Name { get; init; }
    }
}
