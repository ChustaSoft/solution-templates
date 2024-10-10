using $ext_safeprojectname$.Dto.Users;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Application.Users
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto dto);
        Task EditUser(EditUserDto dto);
    }
}
