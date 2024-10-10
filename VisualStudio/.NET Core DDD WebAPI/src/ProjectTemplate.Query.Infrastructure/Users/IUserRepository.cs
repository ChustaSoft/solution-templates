using ProjectTemplate.Query.Dto.Users;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Query.Infrastructure.Users
{
    public interface IUserRepository
    {
        Task<UserProfileReadModel> GetProfileInformationAsync(Guid userId);
    }
}
