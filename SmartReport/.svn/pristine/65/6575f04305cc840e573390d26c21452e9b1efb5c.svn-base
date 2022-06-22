using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Bkav.eGovCloud.DataAccess.Mapping.Admin;
using MySql.Data.MySqlClient;
using StackExchange.Profiling.Data;
using System.Data;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EfAdminContext - public - DAL
    /// Access Modifiers: 
    ///     *Inherit: DbContext
    ///     *Implement: IDbAdminContext
    /// Create Date : 270712
    /// Author      : TrungVH
    /// Description : Giao dịch dữ liệu với db admin
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public class EfAdminContext : DbContext, IDbAdminContext
    {
        private readonly Lazy<EntityQueryFilterProvider> _filterProviderInitializer = new Lazy<EntityQueryFilterProvider>();

        /// <summary>
        /// Khởi tạo class với tham số connection string mặc định trong web.config
        /// </summary>
        public EfAdminContext()
            : base("name=EfAdminContext")
        {
        }

        /// <summary>
        /// Khởi tạo class với tham số là loại connection
        /// </summary>
        /// <param name="connection">Loại connection (SqlConnection, MySqlConnection, OracleConnection)</param>
        public EfAdminContext(DbConnection connection)
            : base(connection, true)
        {
        }

        /// <summary>
        /// Ghi đè lên một số thiết lập mặc định của EF.
        /// </summary>
        /// <param name="modelBuilder">Mô hình db.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
#pragma warning disable 612,618
            modelBuilder.Conventions
                .Remove<System.Data.Entity.Infrastructure.IncludeMetadataConvention>();
#pragma warning restore 612,618

            modelBuilder.Conventions.Remove
                <System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            string databaseType;
            var dbConnection = Database.Connection;
            if (dbConnection is EFProfiledDbConnection)
            {
                var connection = (dbConnection as EFProfiledDbConnection).InnerConnection;
                databaseType = connection is SqlConnection
                                   ? "SqlServer"
                                   : connection is MySqlConnection
                                         ? "MySql"
                                         : "Oracle";
            }
            else
            {
                databaseType = dbConnection is SqlConnection
                                   ? "SqlServer"
                                   : dbConnection is MySqlConnection
                                         ? "MySql"
                                         : "Oracle";
            }

            var typesToRegister = from t in Assembly.GetExecutingAssembly().GetTypes()
                                  where !string.IsNullOrWhiteSpace(t.Namespace) &&
                                  (t.Namespace.Equals("Bkav.eGovCloud.DataAccess.Mapping.Admin")
                                    || t.Namespace.Equals("Bkav.eGovCloud.DataAccess.Mapping.Common")) &&
                                    t.BaseType != null &&
                                    t.BaseType.IsGenericType &&
                                    t.FullName.EndsWith(databaseType, StringComparison.InvariantCultureIgnoreCase)
                                  let genericType = t.BaseType.GetGenericTypeDefinition()
                                  where genericType == typeof(EntityTypeConfiguration<>) || genericType == typeof(ComplexTypeConfiguration<>)
                                  select t;

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            //...hoặc làm bằng tay như ví dụ dưới đây,
            // modelBuilder.Configurations.Add(new CategoryMap());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Lấy ra DbSet cho các kiểu thực thể
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể</typeparam>
        /// <returns></returns>
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        /// <summary>
        /// Lấy ra các repository cho các kiểu thực thể
        /// </summary>
        /// <typeparam name="T">Kiểu thực thể</typeparam>
        /// <returns></returns>
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EfRepository<T>(this);
        }

        /// <summary>
        /// Sử dụng câu query sql để thay đổi dữ liệu
        /// </summary>
        /// <param name="commandText">Câu truy vấn.</param>
        /// <param name="parameters">Các tham số.</param>
        /// <returns></returns>
        public int RawModify(string commandText, params object[] parameters)
        {
            int result;
            try
            {
                using (var command = Database.Connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.AddParams(parameters);
                    Database.Connection.Open();
                    result = command.ExecuteNonQuery();
                }
            }
            finally
            {
                Database.Connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Sử dụng câu query sql để truy vấn dữ liệu
        /// </summary>
        /// <param name="queryText">Câu truy vấn.</param>
        /// <param name="parameters">Các tham số.</param>
        /// <returns>Danh sách kết quả truy vấn</returns>
        public IEnumerable<object> RawQuery(string queryText, params object[] parameters)
        {
            try
            {
                var connection = new MySqlConnection(Database.Connection.ConnectionString);
                using (var command = new MySqlCommand())
                {
                    command.CommandText = queryText;
                    command.Connection = connection;
                    command.AddParams(parameters);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.ToExpandoList();
                    }
                }
            }
            finally
            {
                Database.Connection.Close();
            }
        }

        /// <summary>
        /// Sử dụng câu store để truy vấn dữ liệu
        /// </summary>
        /// <param name="storeName">Câu store.</param>
        /// <param name="parameters">Các tham số.</param>
        /// <returns>Danh sách kết quả truy vấn</returns>
        public IEnumerable<dynamic> RawProcedure(string storeName, params object[] parameters)
        {
            try
            {
                using (var command = Database.Connection.CreateCommand())
                {
                    command.CommandText = storeName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = Database.Connection;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter is DbParameter)
                            {
                                var param = parameter as DbParameter;
                                var newParam = command.CreateParameter();
                                newParam.DbType = param.DbType;
                                newParam.Direction = param.Direction;
                                newParam.IsNullable = param.IsNullable;
                                newParam.ParameterName = param.ParameterName;
                                newParam.Size = param.Size;
                                newParam.SourceColumn = param.SourceColumn;
                                newParam.SourceColumnNullMapping = param.SourceColumnNullMapping;
                                newParam.SourceVersion = param.SourceVersion;
                                newParam.Value = param.Value;
                                command.Parameters.Add(newParam);
                            }
                            else
                            {
                                command.AddParams(parameter);
                            }
                        }
                    }
                    Database.Connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.ToExpandoList();
                    }
                }
            }
            finally
            {
                Database.Connection.Close();
            }
        }

        /// <summary>
        /// Sử dụng câu query sql để truy vấn dữ liệu (lấy ra dữ liệu của cột đầu tiên dòng đầu tiên)
        /// </summary>
        /// <param name="commandText">Câu truy vấn.</param>
        /// <param name="parameters">Các tham số.</param>
        /// <returns></returns>
        public object RawScalar(string commandText, params object[] parameters)
        {
            object result;
            try
            {
                using (var command = Database.Connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.AddParams(parameters);
                    Database.Connection.Open();
                    result = command.ExecuteScalar();
                }
            }
            finally
            {
                Database.Connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Các bộ lọc cho từng nguồn dữ liệu
        /// </summary>
        /// <value>Các bộ lọc.</value>
        public QueryFilterProvider Filters
        {
            get { return _filterProviderInitializer.Value; }
        }

        /// <summary>
        /// Sử dụng câu sql để truy vấn dữ liệu
        /// </summary>
        /// <param name="commandText">Sql query</param>
        /// <param name="parameters">Sql parameter</param>
        /// <returns>Trả về kết quả dạng datatable</returns>
        public DataTable RawTable(string commandText, params object[] parameters)
        {
            DataTable result;
            try
            {
                using (var command = Database.Connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter is DbParameter)
                            {
                                var param = parameter as DbParameter;
                                var newParam = command.CreateParameter();
                                newParam.DbType = param.DbType;
                                newParam.Direction = param.Direction;
                                newParam.IsNullable = param.IsNullable;
                                newParam.ParameterName = param.ParameterName;
                                newParam.Size = param.Size;
                                newParam.SourceColumn = param.SourceColumn;
                                newParam.SourceColumnNullMapping = param.SourceColumnNullMapping;
                                newParam.SourceVersion = param.SourceVersion;
                                newParam.Value = param.Value;
                                command.Parameters.Add(newParam);
                            }
                            else
                            {
                                command.AddParams(parameter);
                            }
                        }
                    }
                    Database.Connection.Open();
                    result = command.ExecuteReader().ToDataTable();
                }
            }
            finally
            {
                Database.Connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Sử dụng Store để truy vấn dữ liệu
        /// </summary>
        /// <param name="commandText">Sql query</param>
        /// <param name="parameters">Sql parameter</param>
        /// <returns>Trả về kết quả dạng datatable</returns>
        public DataTable RawProcedureTable(string commandText, params object[] parameters)
        {
            DataTable result;
            try
            {
                using (var command = Database.Connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter is DbParameter)
                            {
                                var param = parameter as DbParameter;
                                var newParam = command.CreateParameter();
                                newParam.DbType = param.DbType;
                                newParam.Direction = param.Direction;
                                newParam.IsNullable = param.IsNullable;
                                newParam.ParameterName = param.ParameterName;
                                newParam.Size = param.Size;
                                newParam.SourceColumn = param.SourceColumn;
                                newParam.SourceColumnNullMapping = param.SourceColumnNullMapping;
                                newParam.SourceVersion = param.SourceVersion;
                                newParam.Value = param.Value;
                                command.Parameters.Add(newParam);
                            }
                            else
                            {
                                command.AddParams(parameter);
                            }
                        }
                    }
                    Database.Connection.Open();
                    result = command.ExecuteReader().ToDataTable();
                }
            }
            finally
            {
                Database.Connection.Close();
            }
            return result;
        }

    }
}