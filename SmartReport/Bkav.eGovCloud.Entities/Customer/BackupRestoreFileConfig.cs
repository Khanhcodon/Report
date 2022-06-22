
namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : BackupRestoreFileConfig - public - Entity
    /// Access Modifiers: 
    /// Create Date : 130715
    /// Author      : HopCV
    /// Description : Entity tương ứng với bảng BackupRestoreFileConfig trong CSDL
    /// </summary>
    public class BackupRestoreFileConfig
    {
        /// <summary>
        /// Lấy hoặc thiết lập id
        /// </summary>
        public int BackupRestoreFileConfigId { get; set; }

        /// <summary>
        ///  Lấy hoặc thiết lập đang thao tác cho domain nào
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nơi sao lưu file
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên file backup
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tài khoản đăng nhập vào nơi sao lưu file
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khẩu vào nơi sao lưu file
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khẩu khi zip file
        /// </summary>
        public string ZipPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cấu hình có chạy tự động hay không
        /// </summary>
        public bool HasAutoRun { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục được chia sẻ qua mạng
        /// </summary>
        public bool IsNetwork { get; set; }
    }
}
