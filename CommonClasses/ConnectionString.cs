
using System.Configuration;

namespace CommonClasses
{
    public static class ConnectionString
    {
        public static string GetConnectionString(string nameOfConnectionString)
        {
            switch (nameOfConnectionString)
            {
                case "BikeStore":
                    return ConfigurationManager.ConnectionStrings[nameOfConnectionString].ConnectionString;
                default:
                    return null;
            }
        }
    }
}
