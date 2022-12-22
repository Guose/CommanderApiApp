using Commander.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LinqToDB;
using System;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class CommanderRepo : ICommanderRepo
    {
        private readonly CommanderDataConnection _dataConnection;
        public CommanderRepo(CommanderDataConnection dataConnection)
        {
            _dataConnection = dataConnection;
        }

        public async Task<Command> CreateNewCommandAsync(Command cmd)
        {
            await _dataConnection.InsertWithIdentityAsync(cmd ?? throw new NullReferenceException());

            var commandWithNewId = await _dataConnection
                                             .GetTable<Command>()
                                             .OrderByDescending(x => x.Id)
                                             .FirstOrDefaultAsync();
            return commandWithNewId;
        }

        public async Task DeleteCommandAsync(Command cmd)
        {
            await _dataConnection.DeleteAsync(cmd ?? throw new NullReferenceException());
        }

        public async Task<IEnumerable<Command>> GetAllCommandsAsync(bool ascending)
        {
            var rows = _dataConnection.GetTable<Command>();
                
            if (ascending)
                return await rows.OrderBy(c => c.Id).ToListAsync();
            else
                return await rows.OrderByDescending(c => c.Id).ToListAsync();
        }

        public async Task<Command> GetCommandByIdAsync(int id)
        {
            var command = await _dataConnection
                                        .GetTable<Command>()
                                        .Where(c => c.Id == id)
                                        .FirstOrDefaultAsync();

            return command;
        }

        public async Task UpdateCommandAsync(Command cmd)
        {
            await _dataConnection.UpdateAsync(cmd);
        }
    }
}
