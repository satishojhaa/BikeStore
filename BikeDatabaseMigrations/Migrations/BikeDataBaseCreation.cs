using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace BikeDatabaseMigrations.Migrations
{
    [TimestampedMigration(2019,12,14,13,00,"BikeStoreDatabaseCreation")]
    public class BikeDataBaseCreation : Migration
    {
        public override void Down()
        {
            Execute.Script(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SqlScripts",
                "BikeStores Sample Database - drop all objects.sql"));
        }

        public override void Up()
        {
            Execute.Script(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SqlScripts",
                "BikeStores Sample Database - create objects.sql"));
            Execute.Script(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SqlScripts",
                "BikeStores Sample Database - load data.sql"));
        }
    }
}
