using ProjectTemplate.Query.Dto.Users;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Query.Application.Users
{
    public interface IUserService
    {
        Task<UserProfileReadModel> GetUserProfile(Guid userId);
    }
}
