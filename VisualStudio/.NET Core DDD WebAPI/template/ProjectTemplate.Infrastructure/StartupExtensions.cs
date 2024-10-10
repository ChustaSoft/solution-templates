using $ext_safeprojectname$.Framework.Projections;
using $ext_safeprojectname$.Infrastructure.DataStore;
using $ext_safeprojectname$.Infrastructure.DataStore.EventStore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace $ext_safeprojectname$.Infrastructure
{
    public static class StartupExtensions
    {
        public static void InitializeSqlDatabase(this IApplicationBuilder app, string dbConnectionString)
        {
            using var context = new Context(dbConnectionString);

            context.Database.EnsureCreated();

            RemoveReadModel(context);

            RegenerateReadModel(app);
        }

        private static void RemoveReadModel(Context context)
        {
            context.Model
                .GetEntityTypes()
                .Where(t => t.ClrType != typeof(EventData))
                .Select(t => t.GetTableName())
                .ToList()
                .ForEach(readModelTable => context.Database.ExecuteSqlRaw($"delete from {readModelTable}"));
        }

        private static void RegenerateReadModel(IApplicationBuilder app)
        {
            app.ApplicationServices
                .GetService<IReadModelRegenerator>()
                .RegenerateReadModel()
                .Wait();
        }
    }
}
