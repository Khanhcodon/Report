using System;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreConfig - public - Entity
    /// Access Modifiers: 
    /// Create Date : 130715
    /// Author      : HopCV
    /// Description : Entity tương ứng với bảng BackupRestoreConfig trong CSDL
    /// </summary>
    public class BackupRestoreConfig
    {
        /// <summary>
        /// Lấy hoặc thiết lập id của bảng
        /// </summary>
        public int BackupRestoreConfigId { get; set; }

        /// <summary>
        ///  Lấy hoặc thiết lập 
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Địa chỉ ip của server kết nối sao lưu dữ liệu
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Tên cơ sở dữ liệu sao lưu
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại database
        /// </summary>
        public byte DatabaseType { get; set; }

        /// <summary>
        /// Tài khoản đăng nhập vào database
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu kết nối tới database
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Cổng kết nối tới database
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các cấu hình khác
        /// </summary>
        public string Config { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cấu hình có chạy tự động hay không
        /// </summary>
        public bool HasAutoRun { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khảu khi sao lưu rồi zip lại
        /// </summary>
        public string ZipPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nơi lưu trữ file khi sao lưu 
        /// </summary>
        public int ShareFolderId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nơi  lưu trữ file
        /// </summary>
        public virtual ShareFolder ShareFolder { get; set; }
    }

    /// <summary>
    /// Cấu hình cho database là Mysql
    /// </summary>
    public class ConfigInMySql
    {
        /// <summary>
        /// 
        /// </summary>
        public bool HasGetTrigger { get; set; }

        /// <summary>
        /// Có backup các function và thủ tục của database hay không
        /// </summary>
        public bool HasGetFunctionAndProcedure { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục chưa file thưc thị sao lưu phục hồi
        /// </summary>
        public string WorkingDirectory { get; set; }
    }

    /// <summary>
    /// Cấu hình cho database là SqlServer
    /// </summary>
    public class ConfigInSqlServer
    {
        /// <summary>
        /// Lấy hoặc thiết lập back up cách bào  nhieu ngày so với ngày hiện tại
        /// </summary>
        public int? DaySetBackup { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày bắt đầu backup
        /// </summary>
        public DateTime? DateTimeSetBackup { get; set; }
    }

    /// <summary>
    /// Cấu hình cho database là Oracle
    /// </summary>
    public class ConfigInOracle
    {
    }
}
