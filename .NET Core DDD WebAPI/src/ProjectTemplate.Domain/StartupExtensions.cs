using ProjectTemplate.Domain.Aggregates.EmailMessages;
using ProjectTemplate.Domain.Aggregates.Users;
using ProjectTemplate.Domain.Commands.EmailMessages;
using ProjectTemplate.Domain.Commands.Users;
using ProjectTemplate.Domain.DomainServices.Repositories.Users;
using ProjectTemplate.Domain.Events.Users;
using ProjectTemplate.Domain.Policies;
using ProjectTemplate.Domain.Projections;
using ProjectTemplate.Framework.Policies;
using ProjectTemplate.Framework.Projections;
using Microsoft.Extensions.DependencyInjection;
using ProjectTemplate.Framework.Commands;

namespace ProjectTemplate.Domain
{
    public static class StartupExtensions
    {
        public static void ConfigureDomainServices(this IServiceCollection serviceCollection)
        {
            // make sure the aggregate can be found by the DI container
            serviceCollection.AddTransient<User>();

            // register command handlers
            serviceCollection.AddTransient<ICommandHandler<RegisterUserCommand>, User>();
            serviceCollection.AddTransient<ICommandHandler<EditUserCommand>, User>();
            serviceCollection.AddTransient<ICommandHandler<CreateEmailMessageCommand>, EmailMessage>();

            // register domain services
            serviceCollection.AddTransient<IUserRepository, UserRepository>();

            // register projections
            serviceCollection.AddTransient<IProjection<UserRegisteredEvent>, UserProfileInformationProjectiosProjection>();
            serviceCollection.AddTransient<IProjection<UserEditedEvent>, UserProfileInformationProjectiosProjection>();

            // register policies
            serviceCollection.AddTransient<IPolicy<UserRegisteredEvent>, SendWelcomEmailPolicy>();
        }
    }
}