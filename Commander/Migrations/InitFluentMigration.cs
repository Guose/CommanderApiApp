using Commander.Models;
using FluentMigrator;

namespace Commander.Migrations
{
    public class InitFluentMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Commands")
            .WithColumn(nameof(Command.Id)).AsInt64().NotNullable().PrimaryKey().Identity()
            .WithColumn(nameof(Command.HowTo)).AsString().NotNullable()
            .WithColumn(nameof(Command.Line)).AsString().NotNullable()
            .WithColumn(nameof(Command.Platform)).AsString().NotNullable();
        }
    }
}
