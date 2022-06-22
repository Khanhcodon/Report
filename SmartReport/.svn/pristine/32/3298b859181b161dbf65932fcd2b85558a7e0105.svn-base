using System;
using System.Collections.Generic;
using System.Data.Common;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities.Admin;
using StackExchange.Profiling;

namespace Bkav.eGovCloud.Business.Admin
{
    /// <summary>
    /// Class quản lý các kết nối đến cơ sở dữ liệu
    /// </summary>
    public class ConnectionBll : ServiceBase
    {
        private readonly IRepository<Connection> _connectionRepository;
        private readonly ResourceBll _resourceService;
        private const string CONNECTION_STRING_FORMAT = "server={0};User Id={1};password={2};Persist Security Info=True;database={3};Charset=utf8;Port={4};Convert Zero Datetime=true";

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resourceService"></param>
        public ConnectionBll(IDbAdminContext context, ResourceBll resourceService)
            : base(context)
        {
            _connectionRepository = context.GetRepository<Connection>();
            _resourceService = resourceService;
        }

        /// <summary>
        /// Lấy ra tất cả các server
        /// </summary>
        /// <returns>Danh sách các server</returns>
        public IEnumerable<Connection> Gets()
        {
            return _connectionRepository.GetsReadOnly();
        }

        /// <summary>
        /// Thêm mới kết nối cơ sở dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Connection entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.ConnectionRaw = string.Format(CONNECTION_STRING_FORMAT, entity.ServerName, entity.Username, entity.Password, entity.Database, entity.Port);
            if (entity.IsCreateDatabaseIfNotExist)
            {
                var connection = TestConnection(entity);
                var isCreateDatabase = connection == null;
                var provider = DataProviderManager.LoadDataProvider(entity);
                connection = provider.InitDatabase(isCreateDatabase);
                var customerContext = new EfContext(new StackExchange.Profiling.Data.EFProfiledDbConnection(connection, MiniProfiler.Current));
                customerContext.Database.Initialize(true);
                customerContext.SaveChanges();
            }
            _connectionRepository.Create(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về kết nối với database theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Connection Get(int id)
        {
            return _connectionRepository.Get(id);
        }

        /// <summary>
        /// Cập nhật kết nối
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Connection entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.ConnectionRaw = string.Format(CONNECTION_STRING_FORMAT, entity.ServerName, entity.Username, entity.Password, entity.Database, entity.Port);
            Context.SaveChanges();
        }

        /// <summary>
        /// Trả về giá trị xác định kết nối đã tồn tại hay chưa (theo tên)
        /// </summary>
        /// <param name="name">Tên kết nối cần kiểm tra</param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            return _connectionRepository.Exist(c => c.ConnectionName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Tìm kiếm theo tên kết nối
        /// </summary>
        /// <param name="connectionKey"></param>
        /// <returns></returns>
        public IEnumerable<Connection> Search(string connectionKey)
        {
            return _connectionRepository.Gets(true, c => c.ConnectionName.Contains(connectionKey));
        }

        /// <summary>
        /// Xóa kết nối
        /// </summary>
        /// <param name="connection"></param>
        public void Delete(Connection connection)
        {
            if (IsUsed(connection))
            {
                throw new Exception("Kết nối đang được sử dụng");
            }

            _connectionRepository.Delete(connection);
            Context.SaveChanges();
        }

        /// <summary>
        /// Kiểm tra kết nối có đang được sử dụng không
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private bool IsUsed(Connection connection)
        {
            return false;
        }

        /// <summary>
        /// Kiểm tra connection tới CSDL có chính xác hay không
        /// </summary>
        /// <param name="connection">Entity connection</param>
        /// <returns>Trả về DbConnection nếu connection chính xác, ngược lại trả về null</returns>
        /// <exception cref="ArgumentNullException">Ném ngoại lệ khi entity connection truyền vào bị null</exception>
        public DbConnection TestConnection(Connection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            return ConnectionUtil.TestConnection(connection.ConnectionRaw, connection.ServerName, connection.Database,
                connection.Username, connection.Password, connection.Port, connection.DatabaseTypeIdInEnum);
        }
    }
}
