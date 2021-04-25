using ProjectTemplate.Domain.Events.Users;
using ProjectTemplate.Framework.Projections;
using ProjectTemplate.Framework.Repositories;
using ProjectTemplate.Query.Dto.Users;
using System.Threading.Tasks;

namespace ProjectTemplate.Domain.Projections
{
    public class UserProfileInformationProjectiosProjection :
        IProjection<UserRegisteredEvent>,
        IProjection<UserEditedEvent>
    {
        private readonly IReadModelRepository<UserProfileReadModel> readModelUserRepository;

        public UserProfileInformationProjectiosProjection(IReadModelRepository<UserProfileReadModel> readModelUserRepository)
        {
            this.readModelUserRepository = readModelUserRepository;
        }

        public async Task Project(UserRegisteredEvent domainEvent)
        {
            var user = new UserProfileReadModel {
                Id = domainEvent.AggregateId,
                FirstName = domainEvent.FirstName,
                LastName = domainEvent.LastName };

            await readModelUserRepository.Create(user);
        }

        public async Task Project(UserEditedEvent domainEvent)
        {
            await readModelUserRepository.Update(
                updateAction: u => {
                        u.FirstName = domainEvent.FirstName;
                        u.LastName = domainEvent.LastName;
                    },
                filter:       u => u.Id == domainEvent.AggregateId
            );
        }
    }
}
