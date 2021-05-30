using $ext_safeprojectname$.Query.Infrastructure.Users;
using Microsoft.Extensions.DependencyInjection;

namespace $ext_safeprojectname$.Query.Infrastructure
{
    public static class StartupExtensions
    {
        public static void ConfigureReadModelServices(this IServiceCollection serviceCollection, string dbConnectionString)
        {
            serviceCollection.AddTransient<IUserRepository>(x => new UserRepository(dbConnectionString));
        }
    }
}
