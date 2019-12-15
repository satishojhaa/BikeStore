using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDatabaseMigrations
{
    public static class DBConstants
    {
        public const string DATABASE_BIKE_STORE = "BikeStore";
        public const string SCHEMA_PRODUCTION = "production";
        public const string TABLE_BRANDS = "brands";
        public const string TABLE_CATEGORIES = "categories";
        public const string TABLE_PRODUCTS = "products";
        public const string TABLE_STOCKS = "stocks";
        public const string TABLE_LOGS = "logs";

        public const string COL_BRAND_ID = "brand_id";
        public const string COL_BRAND_NAME = "brand_name";

        public const string COL_PRODUCT_ID = "product_id";
        public const string COL_PRODUCT_NAME = "product_name";
        public const string COL_MODEL_YEAR = "model_year";
        public const string COL_LIST_PRICE = "list_price";

        public const string COL_CATEGORY_ID = "category_id";
        public const string COL_CATEGORY_NAME = "category_name";

        public const string COL_STORE_ID = "store_id";
        public const string COL_QUANTITY = "Quantity";

        public const string COL_ID = "Id";
        public const string COL_LOG_MESSAGE = "LogMessage";
        public const string KEY_LOG_PRIMARY_KEY = "PK_Logs";
    }
}
