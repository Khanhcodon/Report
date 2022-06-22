using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using Bkav.eGovCloud.Entities;
using MySql.Data.MySqlClient;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : ConnectionStringBuilder - public - DAL
    /// Access Modifiers:
    /// Create Date : 250912
    /// Author      : TrungVH
    /// Description : Thư viện hỗ trợ sinh chuỗi kết nối đến CSDL
    /// </summary>
    public static class ConnectionUtil
    {
        /// <summary>
        /// Sinh chuỗi kết nối đến Mysql
        /// </summary>
        /// <param name="server">Tên server</param>
        /// <param name="database">Tên database</param>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <param name="port">Cổng</param>
        /// <returns>Chuỗi kết nối</returns>
        public static string GenerateMySqlConnectionString(string server, string database, string username, string password, int? port)
        {
            var builder = new MySqlConnectionStringBuilder
                                          {
                                              Server = server,
                                              UserID = username,
                                              Password = password,
                                              Database = database,
                                              ConvertZeroDateTime = true,
                                              CharacterSet = "utf8",
                                              PersistSecurityInfo = true
                                          };
            if (port.HasValue)
            {
                builder.Port = (uint)port.Value;
            }
            return builder.ConnectionString;

        }

        /// <summary>
        /// Sinh chuỗi kết nối đến SqlServer
        /// </summary>
        /// <param name="server">Tên server</param>
        /// <param name="database">Tên database</param>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>Chuỗi kết nối</returns>
        public static string GenerateSqlConnectionString(string server, string database, string username, string password)
        {
            var builder = new SqlConnectionStringBuilder
            {
                UserID = username,
                Password = password,
                PersistSecurityInfo = true,
                DataSource = server,
                InitialCatalog = database
            };
            return builder.ConnectionString;
        }

        /// <summary>
        /// Kiểm tra chuỗi connection xem có chính xác hay không. Nếu đúng trả về db connection
        /// </summary>
        /// <param name="connectionRaw">Chuỗi connection (nếu dùng tham số này thì các tham số sau sẽ không sử dụng)</param>
        /// <param name="server">Tên server</param>
        /// <param name="database">Tên db</param>
        /// <param name="username">Tên đăng nhập db</param>
        /// <param name="password">Mật khẩu đăng nhập db</param>
        /// <param name="port">Cổng</param>
        /// <param name="databaseType">Kiểu CSDL</param>
        /// <returns></returns>
        public static DbConnection TestConnection(string connectionRaw, string server, string database, string username, string password, short? port, DatabaseType databaseType)
        {
            DbConnection dbConnection = null;
            string connectionString = string.Empty;
            if (!string.IsNullOrWhiteSpace(connectionRaw))
            {
                connectionString = connectionRaw;
            }
            else
            {
                switch (databaseType)
                {
                    case DatabaseType.MySql:
                        connectionString = GenerateMySqlConnectionString(server,
                                                           database,
                                                           username,
                                                           password,
                                                           port.HasValue
                                                               ? int.Parse(
                                                                   port.Value.
                                                                       ToString(CultureInfo.InvariantCulture))
                                                               : (int?)null);
                        break;
                    case DatabaseType.SqlServer:
                        connectionString = GenerateSqlConnectionString(server,
                                                         database,
                                                         username,
                                                         password);
                        break;
                    case DatabaseType.Oracle:
                        //TODO: Them connection string cho oracle
                        break;
                }
            }

            switch (databaseType)
            {
                case DatabaseType.MySql:
                    dbConnection = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.SqlServer:
                    dbConnection = new SqlConnection(connectionString);
                    break;
                //default:
                //dbConnection = new OracleConnection(connectionString);
                //break;
            }
            try
            {
                dbConnection.Open();
                dbConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                dbConnection = null;
            }

            return dbConnection;
        }
    }
}
