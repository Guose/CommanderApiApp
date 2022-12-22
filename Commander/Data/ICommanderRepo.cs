using Commander.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        Task<IEnumerable<Command>> GetAllCommandsAsync(bool ascending);
        Task<Command> GetCommandByIdAsync(int id);
        Task<Command> CreateNewCommandAsync(Command cmd);
        Task UpdateCommandAsync(Command cmd);
        Task DeleteCommandAsync(Command cmd);
    }
}
