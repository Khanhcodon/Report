using MySql.Data.MySqlClient;
using System.Configuration;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// DataObjectFactory caches the connectionstring so that the context can be created quickly.
    /// </summary>
    public static class DataObjectFactory
    {
        private static readonly string ConnectionString;

        /// <summary>
        /// Static constructor. Reads the connectionstring from web.config just once.
        /// </summary>
        static DataObjectFactory()
        {
            //var connectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
            //ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            ConnectionString = ConfigurationManager.ConnectionStrings["EfContext"].ConnectionString;
        }

        /// <summary>
        /// Creates the Context using the current connectionstring.
        /// </summary>
        /// <remarks>
        /// Gof pattern: Factory method. 
        /// </remarks>
        /// <returns>Action Entities context.</returns>
        public static EfContext CreateContext()
        {
            return new EfContext(new MySqlConnection(ConnectionString));//_connectionString
        }
    }
}
