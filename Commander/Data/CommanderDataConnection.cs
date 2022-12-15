using Commander.Models;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace Commander.Data
{
    public class CommanderDataConnection : DataConnection
    {
        public CommanderDataConnection(LinqToDBConnectionOptions<CommanderDataConnection> opt) : base(opt) { }

        public ITable<Command> Commands => this.GetTable<Command>();
    }
}
