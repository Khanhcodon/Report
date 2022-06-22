using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(BackupRestoreFileConfigValidator))]
    public class BackupRestoreFileConfigModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập id
        /// </summary>
        public int BackupRestoreFileConfigId { get; set; }

        /// <summary>
        ///  Lấy hoặc thiết lập đang thao tác cho domain nào
        /// </summary>
        [LocalizationDisplayName("BackupRestoreFileConfig.CreateOrEdit.Fields.Domain.Label")]
        public string Domain { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập nơi sao lưu file
        /// </summary>
        [LocalizationDisplayName("BackupRestoreFileConfig.CreateOrEdit.Fields.Directory.Label")]
        public string Directory { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên file backup
        /// </summary>
        [LocalizationDisplayName("BackupRestoreFileConfig.CreateOrEdit.Fields.FileName.Label")]
        public string FileName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tài khoản đăng nhập vào nơi sao lưu file
        /// </summary>
        [LocalizationDisplayName("BackupRestoreFileConfig.CreateOrEdit.Fields.UserName.Label")]
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khẩu vào nơi sao lưu file
        /// </summary>
        [LocalizationDisplayName("BackupRestoreFileConfig.CreateOrEdit.Fields.Password.Label")]
        public string Password { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  mật khẩu khi zip file
        /// </summary>
        [LocalizationDisplayName("BackupRestoreFileConfig.CreateOrEdit.Fields.ZipPassword.Label")]
        public string ZipPassword { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cấu hình có chạy tự động hay không
        /// </summary>
        [LocalizationDisplayName("BackupRestoreFileConfig.CreateOrEdit.Fields.HasAutoRun.Label")]
        public bool HasAutoRun { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục được chia sẻ qua mạng
        /// </summary>
        [LocalizationDisplayName("BackupRestoreFileConfig.CreateOrEdit.Fields.IsNetwork.Label")]
        public bool IsNetwork { get; set; }
    }
}