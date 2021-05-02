using System.Threading.Tasks;

namespace $ext_safeprojectname$.Framework.Commands
{
    public interface ICommandHandler<T> where T : Command
    {
        Task ProcessCommand(T command);
    }
}
