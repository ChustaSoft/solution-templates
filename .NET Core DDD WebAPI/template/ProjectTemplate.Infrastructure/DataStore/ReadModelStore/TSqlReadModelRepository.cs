using $ext_safeprojectname$.Framework.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace $ext_safeprojectname$.Infrastructure.DataStore.ReadModelStore
{
    public class TSqlReadModelRepository<T> : IReadModelRepository<T> where T : class
    {
        private readonly string _dbConnectionString;

        public TSqlReadModelRepository(string dbConnnectionString)
        {
            _dbConnectionString = dbConnnectionString;
        }

        public async Task Create(T entity)
        {
            using var context = new Context(_dbConnectionString);
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Action<T> updateAction, Expression<Func<T, bool>> filter)
        {
            using var context = new Context(_dbConnectionString);

            var readModels = await context.Set<T>()
                .Where(filter)
                .ToListAsync();

            readModels.ForEach(updateAction);

            await context.SaveChangesAsync();
        }
    }
}

