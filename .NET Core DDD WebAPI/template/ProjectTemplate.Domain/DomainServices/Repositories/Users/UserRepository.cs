using $ext_safeprojectname$.Domain.Aggregates.Users;
using $ext_safeprojectname$.Framework.AggregateRepositories;
using $ext_safeprojectname$.Framework.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Domain.DomainServices.Repositories.Users
{
    public class UserRepository : AggregateRepository<User>, IUserRepository
    {

        public UserRepository(IEventStore eventStore, IServiceProvider serviceProvider) : base(eventStore, serviceProvider)
        {
        }

        public async Task<bool> UserExists(string email)
        {
            var allUsers = await GetAll();
            return allUsers.Any(u => u.Email == email);
        }
    }
}
