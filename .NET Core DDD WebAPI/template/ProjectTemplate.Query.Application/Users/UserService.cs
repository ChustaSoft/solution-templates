using $ext_safeprojectname$.Query.Infrastructure.Users;
using $ext_safeprojectname$.Query.Dto.Users;
using System.Threading.Tasks;
using System;

namespace $ext_safeprojectname$.Query.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserProfileReadModel> GetUserProfile(Guid userId)
        {
            return await userRepository.GetProfileInformationAsync(userId);
        }
    }
}
