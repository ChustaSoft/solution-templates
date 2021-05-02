using $ext_safeprojectname$.Query.Dto.Users;
using System;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Query.Infrastructure.Users
{
    public interface IUserRepository
    {
        Task<UserProfileReadModel> GetProfileInformationAsync(Guid userId);
    }
}
