using $ext_safeprojectname$.Framework.Events;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Framework.Projections
{
    public interface IProjection<T> where T : DomainEvent
    {
        Task Project(T domainEvent);
    }
}
