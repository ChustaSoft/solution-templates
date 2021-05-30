using $ext_safeprojectname$.Framework.Aggregates;
using System;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Framework.AggregateRepositories
{
    public interface IAggregateRepository<T> where T : Aggregate
    {
        Task<T> GetById(Guid id);
    }
}
