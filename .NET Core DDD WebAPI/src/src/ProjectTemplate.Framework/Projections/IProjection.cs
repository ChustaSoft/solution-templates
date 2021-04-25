using ProjectTemplate.Framework.Events;
using System.Threading.Tasks;

namespace ProjectTemplate.Framework.Projections
{
    public interface IProjection<T> where T : DomainEvent
    {
        Task Project(T domainEvent);
    }
}
