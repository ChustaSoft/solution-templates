using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectTemplate.Framework.Repositories
{
    public interface IReadModelRepository<T>
    {
        Task Create(T entity);
        Task Update(Action<T> updateAction, Expression<Func<T, bool>> filter);
    }
}
