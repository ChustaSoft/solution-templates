using System.Threading.Tasks;

namespace $ext_safeprojectname$.Domain.DomainServices.Repositories.Users
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
    }
} 