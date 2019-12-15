using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace BikeDatabaseMigrations.Migrations
{
    [TimestampedMigration(2019, 12, 15, 14, 40, "LogTableCreation")]
    public class LogTableCreation : Migration
    {
        public override void Down()
        {
            Delete.PrimaryKey(DBConstants.KEY_LOG_PRIMARY_KEY).FromTable(DBConstants.TABLE_LOGS);
            Delete.Table(DBConstants.TABLE_LOGS);
        }

        public override void Up()
        {
            Create.Table(DBConstants.TABLE_LOGS)
                .WithColumn(DBConstants.COL_ID).AsInt32().NotNullable().PrimaryKey(DBConstants.KEY_LOG_PRIMARY_KEY).Identity()
                .WithColumn(DBConstants.COL_LOG_MESSAGE).AsCustom("NVARCHAR(512)").NotNullable();
        }
    }
}
