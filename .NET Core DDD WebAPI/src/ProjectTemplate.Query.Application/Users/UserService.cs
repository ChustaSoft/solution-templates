using ProjectTemplate.Query.Infrastructure.Users;
using ProjectTemplate.Query.Dto.Users;
using System.Threading.Tasks;
using System;

namespace ProjectTemplate.Query.Application.Users
{
    /// <summary>
    /// This service is responsible for retrieving information needed by the frontend.
    /// </summary>
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
