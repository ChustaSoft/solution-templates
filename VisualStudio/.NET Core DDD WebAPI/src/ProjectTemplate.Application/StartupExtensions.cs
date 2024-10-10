using ProjectTemplate.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectTemplate.Application
{
    public static class StartupExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUserService, UserService>();
        }
    }
}
