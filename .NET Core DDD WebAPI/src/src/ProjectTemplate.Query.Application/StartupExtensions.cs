using ProjectTemplate.Query.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectTemplate.Query.Application
{
    public static  class StartupExtensions
    {
        public static void ConfigureApplicationReadServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}
