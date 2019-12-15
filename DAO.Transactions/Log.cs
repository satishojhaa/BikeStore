using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeDatabaseMigrations;
using Dapper;

namespace DAO.Transactions
{
    public abstract class Log
    {
        protected void AddLog(string logMessage, IDbConnection connection,IDbTransaction transaction)
        {
            var parameter = new Dictionary<string,object>()
            {
                {DBConstants.COL_LOG_MESSAGE,logMessage }
            };

            var insertQuery = $"INSERT INTO {DBConstants.TABLE_LOGS} ({DBConstants.COL_LOG_MESSAGE}) " +
                              $" VALUES(@{DBConstants.COL_LOG_MESSAGE})";
            connection.Execute(insertQuery, parameter,transaction);
        }
    }
}
