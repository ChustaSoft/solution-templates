using ProjectTemplate.Domain.Aggregates.Users;
using ProjectTemplate.Framework.AggregateRepositories;
using ProjectTemplate.Framework.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Domain.DomainServices.Repositories.Users
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
