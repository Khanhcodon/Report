using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Bkav.eGovCloud.DataAccess.Mapping.Customer;
using MySql.Data.MySqlClient;
using StackExchange.Profiling.Data;

namespace Bkav.eGovCloud.DataAccess
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : EfContext - public - DAL
    /// Access Modifiers: 
    ///     *Inherit: DbContext
    ///     *Implement: IDbCustomerContext
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Giao dịch dữ liệu với db phía khách hàng
    /// </summary>
    [DebuggerStepThrough]
    [System.Runtime.InteropServices.ComVisible(false)]
    public class EfContext : DbContext, IDbCustomerContext
    {
        private readonly Lazy<EntityQueryFilterProvider> _filterProviderInitializer = new Lazy<EntityQueryFilterProvider>();

        /// <summary>
        /// Khởi tạo class với tham số connection string mặc định trong web.config
        /// </summary>
        public EfContext()
            : base("name=EfContext")
        {

        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối đến cơ sở dữ liệu</param>
        public EfContext(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// Khởi tạo class với tham số là loại connection
        /// </summary>
        /// <param name="connection">Loại connection (SqlConnection, MySqlConnection, OracleConnection)</param>
        [DebuggerStepThrough]
        public EfContext(DbConnection connection)
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

            var configType = typeof(UserMapSqlServer);   //một class cấu hình bất kỳ
            var typesToRegister = Assembly.GetAssembly(configType).GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace)
                    && (type.Namespace.Equals("Bkav.eGovCloud.DataAccess.Mapping.Customer")
                                    || type.Namespace.Equals("Bkav.eGovCloud.DataAccess.Mapping.Common"))
                    && !String.IsNullOrEmpty(type.FullName)
                    && type.FullName.EndsWith(databaseType, StringComparison.InvariantCultureIgnoreCase))
            .Where(type => type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            //var typesToRegister = from t in Assembly.GetExecutingAssembly().GetTypes()
            //                      where !string.IsNullOrWhiteSpace(t.Namespace) &&
            //                            t.BaseType != null &&
            //                            t.BaseType.IsGenericType
            //                      let genericType = t.BaseType.GetGenericTypeDefinition()
            //                      where genericType == typeof(EntityTypeConfiguration<>) || genericType == typeof(ComplexTypeConfiguration<>)
            //                      select t;

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            // Admin
            //modelBuilder.Configurations.Add(new DomainMapMySql());
            //modelBuilder.Configurations.Add(new DomainAliasMapMySql());
            //modelBuilder.Configurations.Add(new ConnectionMapMySql());

            //...hoặc làm bằng tay như ví dụ dưới đây,
            //modelBuilder.Configurations.Add(new CategoryMap());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Ghi đè hàm SaveChanges của EF để bắn ra các thông tin sai
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
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
                                newParam.Value = IsDateTime(param.DbType) ? ConvertToDate(param.Value) : param.Value;
                                command.Parameters.Add(newParam);
                            }
                            else
                            {
                                command.AddParams(parameter);
                            }
                        }
                    }
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
        public IEnumerable<dynamic> RawQuery(string queryText, params object[] parameters)
        {
            try
            {
                using (var command = Database.Connection.CreateCommand())
                {
                    command.CommandText = queryText;
                    command.Connection = Database.Connection;
                    command.CommandTimeout = 180;

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
                                newParam.Value = IsDateTime(param.DbType) ? ConvertToDate(param.Value) : param.Value;
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
                    command.CommandTimeout = 2 * 60 * 1000; // 3ph
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
                                newParam.Value = IsDateTime(param.DbType) ? ConvertToDate(param.Value) : param.Value;
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
                        var result = new List<dynamic>();
                        do
                        {
                            result.AddRange(reader.ToExpandoList());
                        } while (reader.NextResult());

                        return result;
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
                                newParam.Value = IsDateTime(param.DbType) ? ConvertToDate(param.Value) : param.Value;
                                command.Parameters.Add(newParam);
                            }
                            else
                            {
                                command.AddParams(parameter);
                            }
                        }
                    }
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
                                newParam.Value = IsDateTime(param.DbType) ? ConvertToDate(param.Value) : param.Value;
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
                                newParam.Value = IsDateTime(param.DbType) ? ConvertToDate(param.Value) : param.Value;
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
        /// Các bộ lọc cho từng nguồn dữ liệu
        /// </summary>
        /// <value>Các bộ lọc.</value>
        public QueryFilterProvider Filters
        {
            get { return _filterProviderInitializer.Value; }
        }

        /// <summary>
        /// Các bộ lọc cho từng nguồn dữ liệu
        /// </summary>
        /// <value>Các bộ lọc.</value>
        public new System.Data.Entity.Infrastructure.DbContextConfiguration Configuration
        {
            get { return base.Configuration; }
        }

        /// <summary>
        /// Chuyển object thành ngày tháng theo định dạng Sql
        /// </summary>
        /// <param name="date">Ngày tháng chuyền vào</param>
        /// <returns></returns>
        private string ConvertToDate(object date)
        {
            var parseDate = DateTime.Parse(date.ToString());
            if (parseDate.Year < 1753)
            {
                parseDate = DateTime.Parse("1753/1/1");
            }
            return parseDate.ToString("yyyy/MM/dd HH:mm:ss");
        }

        /// <summary>
        /// Kiểm tra xem object truyền vào có phải dạng Datetime hay không
        /// </summary>
        /// <param name="dbType">Kiểu dữ liệu sql</param>
        /// <returns></returns>
        private bool IsDateTime(DbType dbType)
        {
            return dbType == DbType.DateTime || dbType == DbType.DateTime2;
        }

    }
}