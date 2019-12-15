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
    public static class StockDal
    {
        public static void UpdateQuantity(int bikeId, int quantity,IDbConnection dbConnection,IDbTransaction dbTransaction)
        {
            var parameter  = new Dictionary<string,object>()
            {
                {DBConstants.COL_PRODUCT_ID,bikeId },
                {DBConstants.COL_QUANTITY, quantity }
            };
            string query = $"UPDATE {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_STOCKS} SET " +
                           $" {DBConstants.COL_QUANTITY}= @{DBConstants.COL_QUANTITY} " +
                           $" WHERE {DBConstants.COL_PRODUCT_ID} = @{DBConstants.COL_PRODUCT_ID}";

            dbConnection.Execute(query, parameter, dbTransaction);
        }

        public static void InsertQuantity(int bikeId, int quantity, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var parameter = new Dictionary<string, object>()
            {
                {DBConstants.COL_PRODUCT_ID,bikeId },
                {DBConstants.COL_QUANTITY, quantity }
            };
            string query = $"INSERT INTO {DBConstants.SCHEMA_PRODUCTION}.{DBConstants.TABLE_STOCKS} " +
                           $" ({DBConstants.COL_STORE_ID},{DBConstants.COL_PRODUCT_ID},{DBConstants.COL_QUANTITY}) " +
                           $" VALUES(1,@{DBConstants.COL_PRODUCT_ID},@{DBConstants.COL_QUANTITY})";

            dbConnection.Execute(query, parameter, dbTransaction);
        }
    }
}
