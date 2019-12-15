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
    public interface IBikeDal
    {
        List<Bike> GetAllBikes();
        Bike GetBikeByName(string bikeName);
        Bike GetBikeById(int bikeId);
        void GetConnectionAndTransaction(out IDbConnection connection, out IDbTransaction transaction);
        void Update(Bike bike, IDbConnection connection=null, IDbTransaction transaction=null);
        Bike Insert(Bike bike, IDbConnection connection = null, IDbTransaction transaction = null);
        void Delete(int bikeId);
    }
    public class BikeDal:Log,IBikeDal
    {
        public virtual IDbConnection Connection
            => new SqlConnection(ConnectionString.GetConnectionString(DBConstants.DATABASE_BIKE_STORE));
        public List<Bike> GetAllBikes()
        {
            string query = GetSelectQuery();

            using (var conn = Connection)
            {
                return conn.Query<Bike>(query).ToList();
            }
        }

        public Bike GetBikeByName(string bikeName)
        {
            var parameter = new Dictionary<string,object>()
            {
                {DBConstants.COL_PRODUCT_NAME,bikeName}
            };
            string query = GetSelectQuery() + $"AND P.{DBConstants.COL_PRODUCT_NAME} = @{DBConstants.COL_PRODUCT_NAME}";

            using (var conn = Connection)
            {
                return conn.Query<Bike>(query, parameter).FirstOrDefault();
            }
        }

        public Bike GetBikeById(int bikeId)
        {
            var parameter = new Dictionary<string, object>()
            {
                {DBConstants.COL_PRODUCT_ID,bikeId}
            };
            string query = GetSelectQuery() + $"AND P.{DBConstants.COL_PRODUCT_ID} = @{DBConstants.COL_PRODUCT_ID}";

            using (var conn = Connection)
            {
                return conn.Query<Bike>(query, parameter).FirstOrDefault();
            }
        }

        public void GetConnectionAndTransaction(out IDbConnection connection, out IDbTransaction transaction)
        {
            var conn = Connection;
            conn.Open();
            connection = conn;
            transaction = conn.BeginTransaction();
        }

        public void Update(Bike bike, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            string updateQuery = $"UPDATE {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_PRODUCTS} SET " +
                                 $" {DBConstants.COL_PRODUCT_NAME}=@{DBConstants.COL_PRODUCT_NAME}, " +
                                 $" {DBConstants.COL_CATEGORY_ID}=@{DBConstants.COL_CATEGORY_ID}, " +
                                 $" {DBConstants.COL_BRAND_ID} = @{DBConstants.COL_BRAND_ID}, " +
                                 $" {DBConstants.COL_LIST_PRICE} = @{DBConstants.COL_LIST_PRICE}, " +
                                 $" {DBConstants.COL_MODEL_YEAR} = @{DBConstants.COL_MODEL_YEAR} " +
                                 $" WHERE {DBConstants.COL_PRODUCT_ID} = @{DBConstants.COL_PRODUCT_ID}";
            var conn = connection ?? Connection;
            conn.Execute(updateQuery, bike, transaction);
            StockDal.UpdateQuantity(bike.Product_Id,bike.Quantity,connection,transaction);
            string logMessage = string.Format(Messages.LogMessage, "Bike", "id", bike.Product_Id, "updated", DateTime.UtcNow);
            AddLog(logMessage, conn, transaction);
        }

        public Bike Insert(Bike bike, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            string insertQuery = $"INSERT INTO {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_PRODUCTS} " +
                                 $"({DBConstants.COL_PRODUCT_NAME},{DBConstants.COL_CATEGORY_ID},{DBConstants.COL_BRAND_ID}, " +
                                 $" {DBConstants.COL_MODEL_YEAR},{DBConstants.COL_LIST_PRICE}) OUTPUT INSERTED.* " +
                                 $" VALUES(@{DBConstants.COL_PRODUCT_NAME},@{DBConstants.COL_CATEGORY_ID},@{DBConstants.COL_BRAND_ID}, " +
                                 $" @{DBConstants.COL_MODEL_YEAR},@{DBConstants.COL_LIST_PRICE})";
            var conn = connection ?? Connection;
            var insertedBike = conn.Query<Bike>(insertQuery, bike, transaction).FirstOrDefault();
            insertedBike.Quantity = bike.Quantity;
            insertedBike.Brand_Name = bike.Brand_Name;
            insertedBike.Category_Name = bike.Category_Name;
            StockDal.InsertQuantity(insertedBike.Product_Id,bike.Quantity,connection,transaction);

            string logMessage = string.Format(Messages.LogMessage, "Bike", "name", bike.Product_Name, "added", DateTime.UtcNow);
            AddLog(logMessage, conn, transaction);

            return insertedBike;
        }

        public void Delete(int bikeId)
        {
            var parameter = new Dictionary<string,object>()
            {
                {DBConstants.COL_PRODUCT_ID,bikeId }
            };
            string deleteQuery = $"DELETE FROM {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_PRODUCTS}" +
                                 $" WHERE {DBConstants.COL_PRODUCT_ID} = @{DBConstants.COL_PRODUCT_ID}";

            using (var conn = Connection)
            {
                conn.Open();
                using (var transaction=conn.BeginTransaction())
                {
                    conn.Execute(deleteQuery, parameter,transaction);
                    string logMessage = string.Format(Messages.LogMessage,"Bike","id",bikeId,"deleted",DateTime.UtcNow);
                    AddLog(logMessage,conn,transaction);
                    transaction.Commit();
                }
                
            }

        }
        private string GetSelectQuery()
        {
            return 
                $"SELECT P.{DBConstants.COL_PRODUCT_ID}, P.{DBConstants.COL_PRODUCT_NAME}, B.{DBConstants.COL_BRAND_NAME}," +
                $" C.{DBConstants.COL_CATEGORY_NAME}, P.{DBConstants.COL_MODEL_YEAR}, P.{DBConstants.COL_LIST_PRICE}, " +
                $" S.{DBConstants.COL_QUANTITY} " +
                $" FROM {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_PRODUCTS} P " +
                $" JOIN {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_BRANDS} B ON B.{DBConstants.COL_BRAND_ID}=P.{DBConstants.COL_BRAND_ID} " +
                $" JOIN {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_STOCKS} S ON P.{DBConstants.COL_PRODUCT_ID}= S.{DBConstants.COL_PRODUCT_ID} " +
                $" JOIN {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_CATEGORIES} C ON C.{DBConstants.COL_CATEGORY_ID} = P.{DBConstants.COL_CATEGORY_ID} " +
                $" WHERE S.{DBConstants.COL_STORE_ID}=1";
        } 
    }
}
