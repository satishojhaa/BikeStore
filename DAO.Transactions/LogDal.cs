using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeDatabaseMigrations;
using CommonClasses;
using Dapper;

namespace DAO.Transactions
{
    public interface ILogDal
    {
        List<string> GetLogs();
    }
    public class LogDal:ILogDal
    {
        public virtual IDbConnection Connection => new SqlConnection(ConnectionString.GetConnectionString(DBConstants.DATABASE_BIKE_STORE));
        public List<string> GetLogs()
        {
            var query = $" SELECT {DBConstants.COL_LOG_MESSAGE} FROM {DBConstants.TABLE_LOGS}";
            using (var conn = Connection)
            {
                return conn.Query<string>(query).ToList();
            }
        } 
    }
}
