using Commander.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LinqToDB;
using System;

namespace Commander.Data
{
    public class CommanderRepo : ICommanderRepo
    {
        private readonly CommanderDataConnection _dataConnection;
        public CommanderRepo(CommanderDataConnection dataConnection)
        {
            _dataConnection = dataConnection;
        }

        public void CreateNewCommand(Command cmd)
        {
            _dataConnection.Insert(cmd ?? throw new NullReferenceException());
        }

        public void DeleteCommand(Command cmd)
        {
            _dataConnection.Delete(cmd ?? throw new NullReferenceException());
        }

        public IEnumerable<Command> GetAllCommands(bool ascending)
        {
            var rows = from c in _dataConnection.Commands
                       select c;
            if (ascending)
                return rows.ToList().OrderBy(c => c.Id);
            else
                return rows.ToList().OrderByDescending(c => c.Id);
        }

        public Command GetCommandById(int id)
        {
            var command = from c in _dataConnection.Commands 
                          where c.Id == id 
                          select c;

            return command.FirstOrDefault();
        }

        public void UpdateCommand(Command cmd)
        {
            _dataConnection.Update(cmd);
        }
    }
}
