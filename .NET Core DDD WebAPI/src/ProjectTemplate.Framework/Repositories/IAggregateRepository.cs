using ProjectTemplate.Framework.Aggregates;
using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Framework.AggregateRepositories
{
    public interface IAggregateRepository<T> where T : Aggregate
    {
        Task<T> GetById(Guid id);
    }
}
