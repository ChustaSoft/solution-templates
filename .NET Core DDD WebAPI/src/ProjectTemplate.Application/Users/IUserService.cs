using ProjectTemplate.Dto.Users;
using System.Threading.Tasks;

namespace ProjectTemplate.Application.Users
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto dto);
        Task EditUser(EditUserDto dto);
    }
}
