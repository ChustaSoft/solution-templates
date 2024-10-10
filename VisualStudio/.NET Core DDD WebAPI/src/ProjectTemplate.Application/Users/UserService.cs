using ProjectTemplate.Domain.Commands.Users;
using ProjectTemplate.Dto.Users;
using System.Threading.Tasks;

namespace ProjectTemplate.Application.Users
{
    /// <summary>
    /// The UserService converts the Dto's to commands. Note that the commands already contain validated information,
    /// as all values are defined as ValueTypes with internal validation.
    ///
    /// After the command has been created the command is processed by the bus (the central processing for events and commands)
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IBus bus;

        public UserService(IBus bus)
        {
            this.bus = bus;
        }

        public async Task EditUser(EditUserDto dto)
        {
            var editUserCommand = new EditUserCommand(dto.UserId, dto.FirstName, dto.LastName, dto.RecoveryEmail);
            await bus.ProcessCommand(editUserCommand);
        }

        public async Task RegisterUser(RegisterUserDto dto)
        {
            var registerUserCommand = new RegisterUserCommand(dto.FirstName, dto.LastName, dto.Email, dto.RecoveryEmail);
            await bus.ProcessCommand(registerUserCommand);
        }
    }
}
