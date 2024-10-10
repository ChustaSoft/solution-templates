using ProjectTemplate.Query.Infrastructure.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectTemplate.Query.Infrastructure
{
    public static class StartupExtensions
    {
        public static void ConfigureReadModelServices(this IServiceCollection serviceCollection, string dbConnectionString)
        {
            serviceCollection.AddTransient<IUserRepository>(x => new UserRepository(dbConnectionString));
        }
    }
}
