using ProjectTemplate.Framework.Projections;
using ProjectTemplate.Infrastructure.DataStore;
using ProjectTemplate.Infrastructure.DataStore.EventStore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ProjectTemplate.Infrastructure
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
