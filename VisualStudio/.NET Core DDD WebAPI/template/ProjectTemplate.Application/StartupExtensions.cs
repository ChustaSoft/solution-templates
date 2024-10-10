using $ext_safeprojectname$.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace $ext_safeprojectname$.Application
{
    public static class StartupExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUserService, UserService>();
        }
    }
}
