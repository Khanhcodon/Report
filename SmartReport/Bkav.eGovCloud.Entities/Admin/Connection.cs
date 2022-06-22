using System;

namespace Bkav.eGovCloud.Entities.Admin
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Connection - public - Entity
    /// Access Modifiers: 
    /// Create Date : 260712
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Connection trong CSDL
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Key
        /// </summary>
        public int ConnectionId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên kết nối
        /// </summary>
        public string ConnectionName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại cơ sở dữ liệu
        /// </summary>
        public byte DatabaseTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên máy chủ cơ sở dữ liệu
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên đăng nhập cơ sở dữ liệu
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mật khẩu đăng nhập cơ sở dữ liệu
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên cơ sở dữ liệu
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Cổng
        /// </summary>
        public Int16? Port { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Chuỗi kết nối (Connection string)
        /// </summary>
        public string ConnectionRaw { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Loại cơ sở dữ liệu (enum)
        /// </summary>
        public Entities.DatabaseType DatabaseTypeIdInEnum
        {
            get { return (Entities.DatabaseType)DatabaseTypeId; }
            set { DatabaseTypeId = (byte)value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Loại cơ sở dữ liệu
        /// </summary>
        public DatabaseType DatabaseType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Domain
        /// </summary>
        public Domain Domain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định database cho eGovOnline
        /// Mặc là cho eGov
        /// </summary>
        public bool IsHsmcDb { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định database cho quản trị tập trung hay cho eGov
        /// </summary>
        public bool IsQuanTriTapTrung { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống sẽ tạo mới 1 database nếu database đó không tồn tại
        /// </summary>
        public bool IsCreateDatabaseIfNotExist { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra hệ thống sẽ reset dữ liệu mặc định
        /// </summary>
        /// <remarks>
        /// Thiết lập này sẽ xóa hết dữ liệu hiện tại của database thì thiết lập dữ liệu mặc định mới
        /// </remarks>
        public bool OverrideCurrentData { get; set; }
    }
}
