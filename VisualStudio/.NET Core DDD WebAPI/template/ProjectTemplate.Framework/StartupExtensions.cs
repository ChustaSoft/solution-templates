using $ext_safeprojectname$.Framework.Projections;
using Microsoft.Extensions.DependencyInjection;

namespace $ext_safeprojectname$.Framework
{
    public static class StartupExtensions
    {
        public static void RegisterDddFrameworkService(this IServiceCollection services)
        {
            services.AddSingleton<IBus, Bus>();
            services.AddTransient<IReadModelRegenerator, ReadModelRegenerator>();
        }
    }
}
