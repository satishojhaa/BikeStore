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
    public interface IBrandDal
    {
        Brand GetBrand(string name);
        int Insert(string name, IDbConnection connection = null, IDbTransaction transaction = null);
        List<Brand> GetAllBrands();
    }
    public class BrandDal : Log,IBrandDal
    {
        public virtual IDbConnection Connection => new SqlConnection(ConnectionString.GetConnectionString(DBConstants.DATABASE_BIKE_STORE));
        public Brand GetBrand(string name)
        {
            var parameter = new Dictionary<string, object>()
            {
                {DBConstants.COL_BRAND_NAME,name}
            };
            string query = GetSelectQuery() + $" WHERE {DBConstants.COL_BRAND_NAME} = @{DBConstants.COL_BRAND_NAME}";

            using (var conn = Connection)
            {
                return conn.Query<Brand>(query, parameter).FirstOrDefault();
            }

        }

        public int Insert(string name, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var parameter = new Dictionary<string, object>()
            {
                {DBConstants.COL_BRAND_NAME,name }
            };
            string query = $"INSERT INTO {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_BRANDS} " +
                           $" ({DBConstants.COL_BRAND_NAME}) OUTPUT INSERTED.{DBConstants.COL_BRAND_ID} " +
                           $" VALUES(@{DBConstants.COL_BRAND_NAME})";

            var conn = connection ?? Connection;
            int id = conn.Query<int>(query, parameter, transaction).SingleOrDefault();
            string logMessage = string.Format(Messages.LogMessage, "Brand", "name", name, "added", DateTime.UtcNow);
            AddLog(logMessage, conn, transaction);

            return id;
        }

        public List<Brand> GetAllBrands()
        {
            using (var conn = Connection)
            {
                return conn.Query<Brand>(GetSelectQuery()).ToList();
            }
        }

        private string GetSelectQuery()
        {
            return $"SELECT {DBConstants.COL_BRAND_ID},{DBConstants.COL_BRAND_NAME} " +
                   $" FROM {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_BRANDS}";
        }
    }
}
