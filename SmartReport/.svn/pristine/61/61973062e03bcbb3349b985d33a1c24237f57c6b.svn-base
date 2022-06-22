using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using Bkav.eGovCloud.Entities.Admin;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlServerDataProvider : BaseEfDataProvider
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connection">Entity connection</param>
        public SqlServerDataProvider(Connection connection) : base(connection)
        {
        }

        #pragma warning disable 1591
        public override DbConnection SetDatabaseInitializer(bool createDatabaseIfNotExist)
        {
            DbConnection dbConnection;
            if(createDatabaseIfNotExist)
            {
                var builder = new SqlConnectionStringBuilder
                {
                    InitialCatalog = "master",
                    DataSource = Connection.ServerName,
                    UserID = Connection.Username,
                    Password = Connection.Password,
                    PersistSecurityInfo = true
                };
                var query = string.Format("CREATE DATABASE [{0}]", Connection.Database);
                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    using (var command = new SqlCommand(query, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                builder.InitialCatalog = Connection.Database;
                dbConnection = new SqlConnection(builder.ConnectionString);
            }
            else
            {
                dbConnection = new SqlConnection(ConnectionUtil
                                                .GenerateSqlConnectionString
                                                (
                                                    Connection.ServerName, 
                                                    Connection.Database, 
                                                    Connection.Username, 
                                                    Connection.Password));
            }

            var customCommands = new List<string>();
            //TODO: Them cac cau lenh o day, co the viet rieng thanh file sql roi doc tu file do ra
            var initializer = new SqlServerDatabaseInitializer(customCommands);

            Database.SetInitializer(initializer);

            return dbConnection;
        }
    }
}
