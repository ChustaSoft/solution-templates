using System.Threading.Tasks;

namespace ProjectTemplate.Domain.DomainServices.Repositories.Users
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
    }
} 