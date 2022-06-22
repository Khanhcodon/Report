using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(BackupRestoreConfigValidator))]
    public class BackupRestoreConfigModel
    {
        public BackupRestoreConfigModel()
        {
            Server = "127.0.0.1";
            Port = 3306;
            HasAutoRun = false;
        }

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
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.Server.Label")]
        public string Server { get; set; }

        /// <summary>
        /// Tên cơ sở dữ liệu sao lưu
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.DatabaseName.Label")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại database
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.DatabaseType.Label")]
        public byte DatabaseType { get; set; }

        /// <summary>
        /// Tài khoản đăng nhập vào database
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.Username.Label")]
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu kết nối tới database
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.Password.Label")]
        public string Password { get; set; }

        /// <summary>
        /// Cổng kết nối tới database
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.Port.Label")]
        public int Port { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các cấu hình khác
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.Config.Label")]
        public string Config { get; set; }

        /// <summary>
        /// Nơi lưu trữ file backup
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.ShareFolderId.Label")]
        public int ShareFolderId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cấu hình có chạy tự động hay không
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.HasAutoRun.Label")]
        public bool HasAutoRun { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khảu khi sao lưu rồi zip lại
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.ZipPassword.Label")]
        public string ZipPassword { get; set; }
    }

    /// <summary>
    /// Cấu hình cho database là Mysql
    /// </summary>
    public class ConfigInMySqlModel
    {
        public ConfigInMySqlModel()
        {
            HasGetTrigger = true;
            HasGetFunctionAndProcedure = true;
        }

        /// <summary>
        /// 
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.HasGetTrigger.Label")]
        public bool HasGetTrigger { get; set; }

        /// <summary>
        /// Có backup các function và thủ tục của database hay không
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.HasGetFunctionAndProcedure.Label")]
        public bool HasGetFunctionAndProcedure { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục chưa file thưc thị sao lưu phục hồi
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.WorkingDirectory.Label")]
        public string WorkingDirectory { get; set; }
    }

    /// <summary>
    /// Cấu hình cho database là SqlServer
    /// </summary>
    public class ConfigInSqlServerModel
    {
        public ConfigInSqlServerModel()
        {
        }

        /// <summary>
        /// Lấy hoặc thiết lập back up cách bào  nhieu ngày so với ngày hiện tại
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.DaySetBackup.Label")]
        public int? DaySetBackup { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày bắt đầu backup
        /// </summary>
        [LocalizationDisplayName("BackupRestoreConfig.CreateOrEdit.Fields.DateTimeSetBackup.Label")]
        public DateTime? DateTimeSetBackup { get; set; }
    }

    /// <summary>
    /// Cấu hình cho database là Oracle
    /// </summary>
    public class ConfigInOracleModel
    {
    }
}