using $ext_safeprojectname$.Domain.Aggregates.EmailMessages;
using $ext_safeprojectname$.Domain.Aggregates.Users;
using $ext_safeprojectname$.Domain.Commands.EmailMessages;
using $ext_safeprojectname$.Domain.Commands.Users;
using $ext_safeprojectname$.Domain.DomainServices.Repositories.Users;
using $ext_safeprojectname$.Domain.Events.Users;
using $ext_safeprojectname$.Domain.Policies;
using $ext_safeprojectname$.Domain.Projections;
using $ext_safeprojectname$.Framework.Policies;
using $ext_safeprojectname$.Framework.Projections;
using Microsoft.Extensions.DependencyInjection;
using $ext_safeprojectname$.Framework.Commands;

namespace $ext_safeprojectname$.Domain
{
    public static class StartupExtensions
    {
        public static void ConfigureDomainServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<User>();
            serviceCollection.AddTransient<ICommandHandler<RegisterUserCommand>, User>();
            serviceCollection.AddTransient<ICommandHandler<EditUserCommand>, User>();
            serviceCollection.AddTransient<ICommandHandler<CreateEmailMessageCommand>, EmailMessage>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<IProjection<UserRegisteredEvent>, UserProfileInformationProjectiosProjection>();
            serviceCollection.AddTransient<IProjection<UserEditedEvent>, UserProfileInformationProjectiosProjection>();
            serviceCollection.AddTransient<IPolicy<UserRegisteredEvent>, SendWelcomEmailPolicy>();
        }
    }
}