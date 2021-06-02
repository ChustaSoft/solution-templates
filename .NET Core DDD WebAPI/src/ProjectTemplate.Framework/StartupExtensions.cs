using ProjectTemplate.Framework.Projections;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectTemplate.Framework
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
