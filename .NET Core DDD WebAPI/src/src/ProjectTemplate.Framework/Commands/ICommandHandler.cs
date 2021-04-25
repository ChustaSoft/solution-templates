using System.Threading.Tasks;

namespace ProjectTemplate.Framework.Commands
{
    public interface ICommandHandler<T> where T : Command
    {
        Task ProcessCommand(T command);
    }
}
