using $ext_safeprojectname$.Query.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace $ext_safeprojectname$.Query.Application
{
    public static  class StartupExtensions
    {
        public static void ConfigureApplicationReadServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}
