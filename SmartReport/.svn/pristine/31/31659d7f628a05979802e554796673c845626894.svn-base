using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using Bkav.eGovCloud.Entities.Admin;
using MySql.Data.MySqlClient;
using DatabaseType = Bkav.eGovCloud.Entities.DatabaseType;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CustomConnectionFactory - public - DAL
    /// Access Modifiers:
    ///     Implement: IDbConnectionFactory
    /// Create Date : 250912
    /// Author      : TrungVH
    /// Description : 1 custom connection factory
    /// </summary>
    public class CustomConnectionFactory : IDbConnectionFactory
    {
        private readonly Connection _connection;

        private Connection BaseConnection
        {
            get
            {
                return _connection;
            }
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connection">Entity Connection</param>
        public CustomConnectionFactory(Connection connection)
        {
            _connection = connection;
        }

        #pragma warning disable 1591
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            DbConnection result = null;
            string connectionString;
            switch (BaseConnection.DatabaseTypeIdInEnum)
            {
                case DatabaseType.MySql:
                    connectionString = ConnectionUtil.GenerateMySqlConnectionString(
                                        BaseConnection.ServerName,
                                        BaseConnection.Database,
                                        BaseConnection.Username,
                                        BaseConnection.Password,
                                        BaseConnection.Port.HasValue
                                        ? int.Parse(BaseConnection.Port.Value.ToString(CultureInfo.InvariantCulture))
                                        : (int?)null);
                    result = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.SqlServer:
                    connectionString = ConnectionUtil.GenerateSqlConnectionString(
                                        BaseConnection.ServerName,
                                        BaseConnection.Database,
                                        BaseConnection.Username,
                                        BaseConnection.Password);
                    result = new SqlConnection(connectionString);
                    break;
                case DatabaseType.Oracle:
                    //TODO: Them ket noi oracle
                    break;
            }
            return result;
        }
    }
}
