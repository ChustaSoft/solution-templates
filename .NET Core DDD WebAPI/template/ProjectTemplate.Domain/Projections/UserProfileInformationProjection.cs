using $ext_safeprojectname$.Domain.Events.Users;
using $ext_safeprojectname$.Framework.Projections;
using $ext_safeprojectname$.Framework.Repositories;
using $ext_safeprojectname$.Query.Dto.Users;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Domain.Projections
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
