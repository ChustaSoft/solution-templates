using System.Threading.Tasks;

namespace ProjectTemplate.Framework.Projections
{
    public interface IReadModelRegenerator
    {
        Task RegenerateReadModel();
    }
}
