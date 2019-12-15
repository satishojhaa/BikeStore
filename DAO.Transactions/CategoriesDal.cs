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
using DAO.Models;

namespace DAO.Transactions
{
    public interface ICategoriesDal
    {
        Category GetCategory(string name);
        int Insert(string name, IDbConnection connection=null, IDbTransaction transaction=null);
        List<Category> GetAllCategories();
    }
    public class CategoriesDal:Log,ICategoriesDal
    {
        public virtual IDbConnection Connection => new SqlConnection(ConnectionString.GetConnectionString(DBConstants.DATABASE_BIKE_STORE));
        public Category GetCategory(string name)
        {
            var parameter = new Dictionary<string,object>()
            {
                {DBConstants.COL_CATEGORY_NAME,name}
            };
            string query = GetSelectQuery() + $" WHERE {DBConstants.COL_CATEGORY_NAME} = @{DBConstants.COL_CATEGORY_NAME}";

            using (var conn = Connection)
            {
                return conn.Query<Category>(query, parameter).FirstOrDefault();
            }

        }

        public int Insert(string name,IDbConnection connection=null,IDbTransaction transaction=null)
        {
            var parameter= new Dictionary<string,object>()
            {
                {DBConstants.COL_CATEGORY_NAME,name }
            };
            string query = $"INSERT INTO {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_CATEGORIES} " +
                           $" ({DBConstants.COL_CATEGORY_NAME}) OUTPUT INSERTED.{DBConstants.COL_CATEGORY_ID} " +
                           $" VALUES(@{DBConstants.COL_CATEGORY_NAME})";

            var conn = connection ?? Connection;
            int id = conn.Query<int>(query, parameter, transaction).SingleOrDefault();
            string logMessage = string.Format(Messages.LogMessage, "Category", "name", name, "added", DateTime.UtcNow);
            AddLog(logMessage, conn, transaction);

            return id;

        }

        public List<Category> GetAllCategories()
        {
            using (var conn=Connection)
            {
                return conn.Query<Category>(GetSelectQuery()).ToList();
            }
        }

        private string GetSelectQuery()
        {
            return $"SELECT {DBConstants.COL_CATEGORY_ID},{DBConstants.COL_CATEGORY_NAME} " +
                   $" FROM {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_CATEGORIES}";
        }
    }
}
