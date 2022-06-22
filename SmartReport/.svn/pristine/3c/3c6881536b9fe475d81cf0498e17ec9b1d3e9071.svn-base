using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Web.Hosting;
using Bkav.eGovCloud.Entities.Admin;
using MySql.Data.MySqlClient;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : MySqlDataProvider - public - DAL
    /// Access Modifiers:
    ///     Inherit: BaseDataProvider
    /// Create Date : 250912
    /// Author      : TrungVH
    /// Description : MySql data provider
    /// </summary>
    public class MySqlDataProvider : BaseEfDataProvider
    {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connection">Entity connection</param>
        public MySqlDataProvider(Connection connection)
            : base(connection)
        {
        }

#pragma warning disable 1591
        public override DbConnection SetDatabaseInitializer(bool createDatabaseIfNotExist)
        {
            DbConnection dbConnection;
            var builder = new MySqlConnectionStringBuilder
            {
                Server = Connection.ServerName,
                Database = "information_schema",
                UserID = Connection.Username,
                Password = Connection.Password,
                PersistSecurityInfo = true,
                ConvertZeroDateTime = true,
                CharacterSet = "utf8"
            };
            if (createDatabaseIfNotExist)
            {
                var query = string.Format("CREATE DATABASE `{0}` CHARACTER SET = 'utf8' COLLATE = 'utf8_unicode_ci'", Connection.Database);
                using (var conn = new MySqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    using (var command = new MySqlCommand(query, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                builder.Database = Connection.Database;
                dbConnection = new MySqlConnection(builder.ConnectionString);
            }
            else
            {
                dbConnection = new MySqlConnection(ConnectionUtil
                                                    .GenerateMySqlConnectionString
                                                    (
                                                        Connection.ServerName,
                                                        Connection.Database,
                                                        Connection.Username,
                                                        Connection.Password,
                                                        Connection.Port.HasValue
                                                        ? int.Parse(Connection.Port.Value.ToString(CultureInfo.InvariantCulture))
                                                        : (int?)null));
            }

            var customCommands = new List<string>();
            if (Connection.OverrideCurrentData)
            {
                if (Connection.IsQuanTriTapTrung)
                {
                    customCommands.AddRange(GetInitSqlCommandForQttt());
                }
                else
                {
                    customCommands.AddRange(GetInitCommandSql());

                    if (Connection.IsHsmcDb)
                    {
                        customCommands.Add(GetSqlCommandHsmc());
                    }
                }
            }

            var initializer = new MySqlDatabaseInitializer(customCommands);
            Database.SetInitializer(initializer);

            return dbConnection;
        }

        private string GetSqlCommandHsmc()
        {
            string result = "";
            // Insert dữ liệu hsmc mặc định
            var scripDataPath = HostingEnvironment.MapPath("~/App_Data/Scripts/MySql/eGovSql/InitializeDefaultData - HSMC.sql");
            if (!string.IsNullOrEmpty(scripDataPath) && File.Exists(scripDataPath))
            {
                result = File.ReadAllText(scripDataPath);
            }

            return result;
        }

        private List<string> GetInitCommandSql()
        {
            var result = new List<string>();
            //TODO: Them cac cau lenh o day, co the viet rieng thanh file sql roi doc tu file do ra
            var scriptModifyTablesPath = HostingEnvironment.MapPath("~/App_Data/Scripts/MySql/eGovSql/InitializeTables.sql");
            if (!string.IsNullOrEmpty(scriptModifyTablesPath) && File.Exists(scriptModifyTablesPath))
            {
                var scriptModifyTablesContents = File.ReadAllText(scriptModifyTablesPath);

                result.Add(scriptModifyTablesContents);
            }

            // Insert các function và store mặc định
            var scripStorePath = HostingEnvironment.MapPath("~/App_Data/Scripts/MySql/eGovSql/InitializeFunctions.sql");
            if (!string.IsNullOrEmpty(scripStorePath) && File.Exists(scripStorePath))
            {
                var scriptStoreContents = File.ReadAllText(scripStorePath);

                result.Add(scriptStoreContents);
            }

            // Insert dữ liệu mặc định
            var scripDataPath = HostingEnvironment.MapPath("~/App_Data/Scripts/MySql/eGovSql/InitializeDefaultData.sql");
            if (!string.IsNullOrEmpty(scripDataPath) && File.Exists(scripDataPath))
            {
                var scriptDataContents = File.ReadAllText(scripDataPath);

                result.Add(scriptDataContents);
            }


            return result;
        }

        private List<string> GetInitSqlCommandForQttt()
        {
            var result = new List<string>();
            //TODO: Them cac cau lenh o day, co the viet rieng thanh file sql roi doc tu file do ra
            var scriptModifyTablesPath = HostingEnvironment.MapPath("~/App_Data/Scripts/MySql/QTTTT/InitializeTables.sql");
            if (!string.IsNullOrEmpty(scriptModifyTablesPath) && File.Exists(scriptModifyTablesPath))
            {
                var scriptModifyTablesContents = File.ReadAllText(scriptModifyTablesPath);

                result.Add(scriptModifyTablesContents);
            }

            return result;
        }
    }
}
