using $ext_safeprojectname$.Query.Dto.Users;
using System;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Query.Application.Users
{
    public interface IUserService
    {
        Task<UserProfileReadModel> GetUserProfile(Guid userId);
    }
}
